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
        // Задача которую тащим
        private TaskModel? _draggedTask;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Конструктор для совместимости
        public MainWindow(AuthService authService) : this()
        {
        }

        // -------------------------------------------------------
        // Drag — начало перетаскивания при клике на карточку
        // -------------------------------------------------------
        private void TaskCard_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is TaskModel task)
            {
                _draggedTask = task;

                // Визуальный эффект увеличения при захвате
                var scale = new ScaleTransform(1.05, 1.05);
                border.RenderTransform = scale;
                border.RenderTransformOrigin = new Point(0.5, 0.5);

                var anim = new DoubleAnimation(1.05, 1.08,
                    new Duration(System.TimeSpan.FromMilliseconds(150)));
                scale.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
                scale.BeginAnimation(ScaleTransform.ScaleYProperty, anim);

                // Запускаем drag & drop
                DragDrop.DoDragDrop(border, task, DragDropEffects.Move);

                // Возврат размера после отпускания
                var animBack = new DoubleAnimation(1.0,
                    new Duration(System.TimeSpan.FromMilliseconds(150)));
                scale.BeginAnimation(ScaleTransform.ScaleXProperty, animBack);
                scale.BeginAnimation(ScaleTransform.ScaleYProperty, animBack);

                _draggedTask = null;
            }
        }

        // -------------------------------------------------------
        // DragOver — подсвечиваем куда можно бросить
        // -------------------------------------------------------
        private void TaskListView_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
            e.Handled = true;
        }

        // -------------------------------------------------------
        // Drop — переставляем задачу в новую позицию
        // -------------------------------------------------------
        private void TaskListView_Drop(object sender, DragEventArgs e)
        {
            if (_draggedTask == null) return;
            if (DataContext is not TaskManagerViewModel vm) return;

            // Находим элемент под курсором
            var target = GetTaskUnderCursor(e.GetPosition(TaskListView));

            if (target != null && target != _draggedTask)
            {
                int oldIndex = vm.Tasks.IndexOf(_draggedTask);
                int newIndex = vm.Tasks.IndexOf(target);

                if (oldIndex >= 0 && newIndex >= 0)
                    vm.Tasks.Move(oldIndex, newIndex);
            }

            _draggedTask = null;
        }

        // -------------------------------------------------------
        // Вспомогательный метод — найти задачу под курсором
        // -------------------------------------------------------
        private TaskModel? GetTaskUnderCursor(System.Windows.Point position)
        {
            var hit = VisualTreeHelper.HitTest(TaskListView, position);
            if (hit == null) return null;

            DependencyObject? obj = hit.VisualHit;

            while (obj != null)
            {
                if (obj is FrameworkElement fe && fe.DataContext is TaskModel task)
                    return task;

                obj = VisualTreeHelper.GetParent(obj);
            }

            return null;
        }
    }
}