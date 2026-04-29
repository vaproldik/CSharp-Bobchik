public class BusFactory : VehicleFactory
{
    public override IVehicle CreateVehicle()
    {
        return new Bus();
    }
}
