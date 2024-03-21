class Account {
    readonly owener: string;
    private _state: State;

    constructor(owener: string) {
        this.owener = owener;
        this._state = new SilverState(0, this);

        console.log(`Account of ${this.owener} was created.`);
        console.log(`\tBalance: ${this._state.balance.toFixed(2)}`);
        console.log(`\tStatus: ${(this._state.constructor as any).name}`);
    }

    public get balance(): number {
        return this._state.balance;
    }

    public set state(state: State) {
        this._state = state;
    }

    public deposit(amount: number): void
    {
        this._state.deposit(amount);

        console.log(`Deposited ${amount.toFixed(2)}`);
        console.log(`\tBalance = ${this._state.balance.toFixed(2)}`);
        console.log(`\tStatus: ${(this._state.constructor as any).name}`);
    }

    public withdraw(amount: number): void
    {
        this._state.withdraw(amount);

        console.log(`Withdrew ${amount.toFixed(2)}`);
        console.log(`\tBalance = ${this._state.balance.toFixed(2)}`);
        console.log(`\tStatus: ${(this._state.constructor as any).name}`);
    }
}

abstract class State
{
    account: Account;
    balance: number;

    public abstract deposit(amout: number): void;
    public abstract withdraw(amount: number): void;
}

class RedState extends State
{
    constructor(state: State) {
        super();

        this.balance = state.balance;
        this.account = state.account;
    }
    public deposit(amount: number): void {
        this.balance += amount;
        this.checkState();
    }

    public withdraw(amount: number): void {
        console.log('### NO FOUNDS AVAILABLE FOR WITHDRAW ###');
    }

    private checkState(): void
    {
        if(this.balance > 0)
        {
            this.account.state = new SilverState(this);
        }
    }
}

class SilverState extends State
{
    constructor(balance: number, account: Account);
    constructor(state: State);
    constructor(balanceOrState: number | State, account?: Account) {
        super();

        if (balanceOrState instanceof State) {
            this.balance = balanceOrState.balance;
            this.account = balanceOrState.account;
        } else {
            this.balance = balanceOrState;
            this.account = account!;
        }
    }

    public deposit(amount: number): void {
        this.balance += amount;
        this.checkState();
    }

    public withdraw(amount: number): void
    {
        var fee = 0.05;
        var feeAmount = amount * fee;

        this.balance -= amount + feeAmount;

        this.checkState();
    }

    private checkState(): void
    {
        if(this.balance < 0)
        {
            this.account.state = new RedState(this);
        }

        else if(this.balance >= 1000.0)
        {
            this.account.state  = new GoldState(this);
        }
    }
}

class GoldState extends State
{
    constructor(state: State) {
        super();

        this.balance = state.balance;
        this.account = state.account;
    }
    public deposit(amount: number): void {
        this.balance += amount;
        this.checkState();
    }

    public withdraw(amount: number): void {
        this.balance -= amount;

        this.checkState();
    }

    private checkState(): void
    {
        if(this.balance < 0)
        {
            this.account.state = new RedState(this);
        }

        else if(this.balance < 1000.0)
        {
            this.account.state  = new SilverState(this);
        }
    }
}

let account = new Account("Nelson Nobre");

account.deposit(500);
account.withdraw(100);

account.deposit(300);
account.deposit(550);

account.withdraw(2000);
account.withdraw(1100);
