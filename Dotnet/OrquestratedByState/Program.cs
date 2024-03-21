// Client
var account = new Account("Nelson Nobre");

account.Deposit(500.0);
account.Withdraw(100.0);

account.Deposit(300.0);
account.Deposit(550.0);

account.Withdraw(2000.00);
account.Withdraw(1100.00);


/// <summary>
/// The 'Context' class
/// </summary>
public class Account
{
    public string Owner { get; private set; }
    public State State { get; set; }
    public double Balance => State.Balance;

    public Account(string owner)
    {
        // New accounts are 'Silver' by default
        Owner = owner;
        State = new SilverState(0.0, this);

        Console.WriteLine("Created account for {0}", Owner);
        Console.WriteLine("\tBalance = {0:C}", Balance);
        Console.WriteLine("\tStatus  = {0}", State.GetType().Name);
    }

    public void Deposit(double amount)
    {
        State.Deposit(amount);

        Console.WriteLine("Deposited {0:C}", amount);
        Console.WriteLine("\tBalance = {0:C}", Balance);
        Console.WriteLine("\tStatus  = {0}", State.GetType().Name);
    }

    public void Withdraw(double amount)
    {
        State.Withdraw(amount);

        Console.WriteLine("Withdrew {0:C}", amount);
        Console.WriteLine("\tBalance = {0:C}", Balance);
        Console.WriteLine("\tStatus  = {0}", State.GetType().Name);
    }
}


/// <summary>
/// The 'State' abstract class
/// </summary>
public abstract class State
{
    public Account Account { get; protected set; } = default!;
    public double Balance { get; protected set; }


    public abstract void Deposit(double amount);
    public abstract void Withdraw(double amount);
}



/// <summary>
/// A 'ConcreteState' class
/// </summary>
public class RedState : State
{
    public RedState(State state)
    {
        Balance = state.Balance;
        Account = state.Account;
    }

    public override void Deposit(double amount)
    {
        Balance += amount;
        _checkState();
    }

    public override void Withdraw(double amount)
        => Console.WriteLine("### NO FUNDS AVAILABLE FOR WITHDRAWAL! ###");

    private void _checkState()
    {
        if(Balance >= 0)
        {
            Account.State = new SilverState(this);
        }
    }
}

/// <summary>
/// A 'ConcreteState' class
/// </summary>
public class SilverState : State
{
    public SilverState(double balance, Account account)
    {
        Balance = balance;
        Account = account;
    }

    public SilverState(State state)
        : this(state.Balance, state.Account)
    { }



    public override void Deposit(double amount)
    {
        Balance += amount;
        _checkState();
    }

    public override void Withdraw(double amount)
    {
        var fee = 0.05;
        var feeAmount = amount * fee;

        Balance -= amount + feeAmount;

        _checkState();
    }

    private void _checkState()
    {
        if(Balance < 0)
        {
            Account.State = new RedState(this);
        }

        else if(Balance >= 1000.0)
        {
            Account.State = new GoldState(this);
        }
    }
}

/// <summary>
/// A 'ConcreteState' class
/// </summary>
public class GoldState : State
{
    public GoldState(State state)
    {
        Balance = state.Balance;
        Account = state.Account;
    }


    public override void Deposit(double amount)
    {
        Balance += amount;
        _checkState();
    }

    public override void Withdraw(double amount)
    {
        Balance -= amount;
        _checkState();
    }

    private void _checkState()
    {
        if(Balance < 0.0)
        {
            Account.State = new RedState(this);
        }

        else if(Balance < 1000.0)
        {
            Account.State = new SilverState(this);
        }
    }
}
