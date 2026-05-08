using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Task1.Models;
using Task1.Services;
using Task1.ViewModels;

namespace Task1
{
    public partial class MainWindow : Window
    {
        private TaskItem? _draggedTask;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(AuthService authService) : this() { }

        // Двойной клик — редактирование
        private void TaskCard_MouseLeftButtonDown(
            object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (DataContext is TaskManagerViewModel vm)
                    vm.EditCommand.Execute(null);
                return;
            }

            // Одинарный клик — начало drag
            if (sender is Border border &&
                border.DataContext is TaskItem task)
            {
                _draggedTask = task;

                var scale = new ScaleTransform(1.0, 1.0);
                border.RenderTransform = scale;
                border.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);

                var up = new DoubleAnimation(1.08,
                    new Duration(System.TimeSpan.FromMilliseconds(120)));
                scale.BeginAnimation(ScaleTransform.ScaleXProperty, up);
                scale.BeginAnimation(ScaleTransform.ScaleYProperty, up);

                DragDrop.DoDragDrop(border, task, DragDropEffects.Move);

                var down = new DoubleAnimation(1.0,
                    new Duration(System.TimeSpan.FromMilliseconds(120)));
                scale.BeginAnimation(ScaleTransform.ScaleXProperty, down);
                scale.BeginAnimation(ScaleTransform.ScaleYProperty, down);

                _draggedTask = null;
            }
        }

        private void TaskListBox_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
            e.Handled = true;
        }

        private void TaskListBox_Drop(object sender, DragEventArgs e)
        {
            if (_draggedTask == null) return;
            if (DataContext is not TaskManagerViewModel vm) return;

            var target = GetTaskUnder(e.GetPosition(TaskListBox));

            if (target != null && target != _draggedTask)
            {
                int from = vm.Tasks.IndexOf(_draggedTask);
                int to = vm.Tasks.IndexOf(target);

                if (from >= 0 && to >= 0)
                    vm.Tasks.Move(from, to);
            }

            _draggedTask = null;
        }

        private TaskItem? GetTaskUnder(System.Windows.Point pos)
        {
            var hit = VisualTreeHelper.HitTest(TaskListBox, pos);
            if (hit == null) return null;

            DependencyObject? obj = hit.VisualHit;
            while (obj != null)
            {
                if (obj is FrameworkElement fe &&
                    fe.DataContext is TaskItem task)
                    return task;

                obj = VisualTreeHelper.GetParent(obj);
            }

            return null;
        }
    }
}