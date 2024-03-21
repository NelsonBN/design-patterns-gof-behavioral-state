# GoF - Behavioral - State

[Home](../../../README.md)


- [UML Diagram](#uml-diagram)
- [State orquestration](#state-orquestration)
- [Pos and Cons](#pos-and-cons)
- [References](#references)



## UML Diagram
![State](https://www.dofactory.com/img/diagrams/net/state.png)


## State orquestration

The state pattern isn't opinionated about how the state is orquestrated. We can implement it in three different ways:

- **Orquestrated by the context**: The context is responsible for changing the state.
- **Orquestrated by the state**: The states are responsible for changing the state to another state/next state.
- **Orquestrated by client**: The client is responsible for changing the state. The usualy pass the context as a parameter to the state.


## Pos and Cons

- ✅ Clean code
- ✅ Single Responsibility Principle
- ✅ Open/Closed Principle
- ✅ Easy to add new states
- ❌ Overkill for simple cases (When there are only a few states)


## References
- [doFactory](https://www.dofactory.com/net/state-design-pattern)
