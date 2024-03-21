class Account:
    # The 'Context' class
    def __init__(self, owner):
        self.owner = owner
        self.state = SilverState(0.0, self)

        print(f"Created account for {self.owner}")
        print(f"\tBalance = {self.state.balance}")
        print(f"\tStatus  = {self.state.__class__.__name__}")

    def deposit(self, amount):
        self.state.deposit(amount)

        print(f"Deposited {amount}")
        print(f"\tBalance = {self.state.balance}")
        print(f"\tStatus  = {self.state.__class__.__name__}")

    def withdraw(self, amount):
        self.state.withdraw(amount)

        print(f"Withdrew {amount}")
        print(f"\tBalance = {self.state.balance}")
        print(f"\tStatus  = {self.state.__class__.__name__}")


class State:
    # The 'State' abstract class
    def __init__(self):
        self.account = None
        self.balance = 0

    def deposit(self, amount):
        pass

    def withdraw(self, amount):
        pass

class RedState(State):
    def __init__(self, state):
        self.balance = state.balance
        self.account = state.account

    def deposit(self, amount):
        self.balance += amount
        self.checkState()

    def withdraw(self, amount):
        print("### NO FUNDS AVAILABLE FOR WITHDRAWAL! ###")

    def checkState(self):
        if self.balance >= 0:
            self.account.state = SilverState(self)

class SilverState(State):
    # The 'ConcreteState' class
    def __init__(self, balance, account):
        self.balance = balance
        self.account = account

    def deposit(self, amount):
        self.balance += amount
        self.checkState()

    def withdraw(self, amount):
        fee = 0.05
        feeAmount = amount * fee

        self.balance -= amount + feeAmount

        self.checkState()

    def checkState(self):
        if self.balance < 0:
            self.account.state = RedState(self)
        elif self.balance >= 1000.0:
            self.account.state = GoldState(self)

class GoldState(State):
    # The 'ConcreteState' class
    def __init__(self, state):
        self.balance = state.balance
        self.account = state.account

    def deposit(self, amount):
        self.balance += amount
        self.check_state()

    def withdraw(self, amount):
        self.balance -= amount
        self.check_state()

    def check_state(self):
        if self.balance < 0.0:
            self.account.state = RedState(self)
        elif self.balance < 1000.0:
            self.account.state = SilverState(self)



# Example usage (Client)
account = Account("Nelson Nobre")
account.deposit(500)
account.withdraw(100)

account.deposit(300)
account.deposit(550)

account.withdraw(2000)
account.withdraw(1100)
