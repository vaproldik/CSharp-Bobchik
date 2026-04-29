public class BicycleFactory : VehicleFactory
{
    public override IVehicle CreateVehicle()
    {
        return new Bicycle();
    }
}
