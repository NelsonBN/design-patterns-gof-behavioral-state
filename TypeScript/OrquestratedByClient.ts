// State
interface IDrivingMode
{
    throttle(): void;
    brake(): void;
}

// Context
class Car
{
    public speed: number;
    private _drivingMode: IDrivingMode;

    constructor()
    {
        this.speed = 0;
        this._drivingMode = new EcoDrivingMode(this);
    }

    public throttle(): void
    {
        this._drivingMode.throttle();
    }

    public brake(): void
    {
        this._drivingMode.brake();
    }

    public set drivingMode(mode: IDrivingMode)
    {
        this._drivingMode = mode;
    }
}

// Concrete States
class EcoDrivingMode implements IDrivingMode
{
    constructor(private car: Car) {}

    throttle(): void
    {
        this.car.speed += 5;
        console.log(`EcoDrivingMode -> throttle: ${this.car.speed}km/h`);
    }

    brake(): void
    {
        this.car.speed -= 5;
        if (this.car.speed < 0) this.car.speed = 0;
        console.log(`EcoDrivingMode -> brake: ${this.car.speed}km/h`);
    }
}

// Concrete States
class ComfortDrivingMode implements IDrivingMode
{
    constructor(private car: Car) {}

    throttle(): void
    {
        this.car.speed += 10;
        console.log(`ComfortDrivingMode -> throttle: ${this.car.speed}km/h`);
    }

    brake(): void
    {
        this.car.speed -= 10;
        if (this.car.speed < 0) this.car.speed = 0;
        console.log(`ComfortDrivingMode -> brake: ${this.car.speed}km/h`);
    }
}

// Concrete States
class SportDrivingMode implements IDrivingMode
{
    constructor(private car: Car) {}

    throttle(): void
    {
        this.car.speed += 15;
        console.log(`SportDrivingMode -> throttle: ${this.car.speed}km/h`);
    }

    brake(): void
    {
        this.car.speed -= 15;
        if (this.car.speed < 0) this.car.speed = 0;
        console.log(`SportDrivingMode -> brake: ${this.car.speed}km/h`);
    }
}

// Client
var car = new Car();

car.brake();
car.throttle();
car.throttle();
car.brake();
car.throttle();
car.throttle();

car.drivingMode = new ComfortDrivingMode(car);

car.brake();
car.throttle();
car.throttle();
car.throttle();
car.brake();

car.drivingMode = new SportDrivingMode(car);
car.brake();
car.throttle();
car.throttle();
car.throttle();
car.brake();
