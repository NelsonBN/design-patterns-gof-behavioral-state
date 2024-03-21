// Client
var car = new Car();

car.Brake();
car.Throttle();
car.Throttle();
car.Brake();
car.Throttle();
car.Throttle();


car.DrivingMode(new ComfortDrivingMode(car));

car.Brake();
car.Throttle();
car.Throttle();
car.Throttle();
car.Brake();

car.DrivingMode(new SportDrivingMode(car));
car.Brake();
car.Throttle();
car.Throttle();
car.Throttle();
car.Brake();


// State
interface IDrivingMode
{
    void Throttle();
    void Brake();
}

// Context
class Car
{
    public int Speed;
    private IDrivingMode _drivingMode;

    public Car()
    {
        Speed = 0;
        _drivingMode = new EcoDrivingMode(this);
    }

    public void Throttle()
    {
        _drivingMode.Throttle();
    }

    public void Brake()
    {
        _drivingMode.Brake();
    }

    public void DrivingMode(IDrivingMode mode)
    {
        _drivingMode = mode;
    }
}

// Concrete States
class EcoDrivingMode : IDrivingMode
{
    private readonly Car _car;

    public EcoDrivingMode(Car car) => _car = car;

    public void Throttle()
    {
        _car.Speed += 5;
        Console.WriteLine($"EcoDrivingMode -> throttle: {_car.Speed}km/h");
    }

    public void Brake()
    {
        _car.Speed -= 5;
        if (_car.Speed < 0) _car.Speed = 0;
        Console.WriteLine($"EcoDrivingMode -> brake: {_car.Speed}km/h");
    }
}

// Concrete States
class ComfortDrivingMode : IDrivingMode
{
    private readonly Car _car;

    public ComfortDrivingMode(Car car) => _car = car;

    public void Throttle()
    {
        _car.Speed += 10;
        Console.WriteLine($"ComfortDrivingMode -> throttle: {_car.Speed}km/h");
    }

    public void Brake()
    {
        _car.Speed -= 10;
        if (_car.Speed < 0) _car.Speed = 0;
        Console.WriteLine($"ComfortDrivingMode -> brake: {_car.Speed}km/h");
    }
}

// Concrete States
class SportDrivingMode : IDrivingMode
{
    private readonly Car _car;

    public  SportDrivingMode(Car car) => _car = car;

    public void Throttle()
    {
        _car.Speed += 15;
        Console.WriteLine($"SportDrivingMode -> throttle: {_car.Speed}km/h");
    }

    public void Brake()
    {
        _car.Speed -= 15;
        if (_car.Speed < 0) _car.Speed = 0;
        Console.WriteLine($"SportDrivingMode -> brake: {_car.Speed}km/h");
    }
}
