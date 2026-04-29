class Program
{
    static void Main(string[] args)
    {
        VehicleFactory factory;

        factory = new CarFactory();
        IVehicle car = factory.CreateVehicle();
        car.Move();

        factory = new BusFactory();
        IVehicle bus = factory.CreateVehicle();
        bus.Move();

        factory = new BicycleFactory();
        IVehicle bike = factory.CreateVehicle();
        bike.Move();
    }
}