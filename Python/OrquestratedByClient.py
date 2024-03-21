# State
class IDrivingMode:
    def throttle(self):
        pass

    def brake(self):
        pass

# Context
class Car:
    def __init__(self):
        self.speed = 0
        self._driving_mode = EcoDrivingMode(self)

    def throttle(self):
        self._driving_mode.throttle()

    def brake(self):
        self._driving_mode.brake()

    def driving_mode(self, mode):
        self._driving_mode = mode

# Concrete States
class EcoDrivingMode(IDrivingMode):
    def __init__(self, car):
        self._car = car

    def throttle(self):
        self._car.speed += 5
        print(f"EcoDrivingMode -> throttle: {self._car.speed}km/h")

    def brake(self):
        self._car.speed -= 5
        self._car.speed = max(self._car.speed, 0)
        print(f"EcoDrivingMode -> brake: {self._car.speed}km/h")

class ComfortDrivingMode(IDrivingMode):
    def __init__(self, car):
        self._car = car

    def throttle(self):
        self._car.speed += 10
        print(f"ComfortDrivingMode -> throttle: {self._car.speed}km/h")

    def brake(self):
        self._car.speed -= 10
        self._car.speed = max(self._car.speed, 0)
        print(f"ComfortDrivingMode -> brake: {self._car.speed}km/h")

class SportDrivingMode(IDrivingMode):
    def __init__(self, car):
        self._car = car

    def throttle(self):
        self._car.speed += 15
        print(f"SportDrivingMode -> throttle: {self._car.speed}km/h")

    def brake(self):
        self._car.speed -= 15
        self._car.speed = max(self._car.speed, 0)
        print(f"SportDrivingMode -> brake: {self._car.speed}km/h")

# Client
car = Car()

car.brake()
car.throttle()
car.throttle()
car.brake()
car.throttle()
car.throttle()

car.driving_mode(ComfortDrivingMode(car))

car.brake()
car.throttle()
car.throttle()
car.throttle()
car.brake()

car.driving_mode(SportDrivingMode(car))
car.brake()
car.throttle()
car.throttle()
car.throttle()
car.brake()
