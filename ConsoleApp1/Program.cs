using System;
using System.Collections.Generic;

// Абстрактный класс продукта
public abstract class Product
{
    public abstract void Accept(Visitor visitor);
}

// Конкретные классы продуктов
public class ConcreteProductA : Product
{
    public override void Accept(Visitor visitor)
    {
        visitor.VisitConcreteProductA(this);
    }

    public void OperationA()
    {
        Console.WriteLine("Выполнение операции A для продукта A");
    }
}

public class ConcreteProductB : Product
{
    public override void Accept(Visitor visitor)
    {
        visitor.VisitConcreteProductB(this);
    }

    public void OperationB()
    {
        Console.WriteLine("Выполнение операции B для продукта B");
    }
}

// Интерфейс посетителя
public interface Visitor
{
    void VisitConcreteProductA(ConcreteProductA productA);
    void VisitConcreteProductB(ConcreteProductB productB);
}

// Конкретный посетитель
public class ConcreteVisitor : Visitor
{
    public void VisitConcreteProductA(ConcreteProductA productA)
    {
        productA.OperationA();
    }

    public void VisitConcreteProductB(ConcreteProductB productB)
    {
        productB.OperationB();
    }
}

// Абстрактный класс фабрики
public abstract class Creator
{
    public abstract Product FactoryMethod();

    public void Operation()
    {
        Product product = FactoryMethod();
        product.Accept(new ConcreteVisitor());
    }
}

// Конкретные классы фабрик
public class ConcreteCreatorA : Creator
{
    public override Product FactoryMethod()
    {
        return new ConcreteProductA();
    }
}

public class ConcreteCreatorB : Creator
{
    public override Product FactoryMethod()
    {
        return new ConcreteProductB();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Creator creatorA = new ConcreteCreatorA();
        creatorA.Operation();
        // Вывод:
        // Выполнение операции A для продукта A

        Creator creatorB = new ConcreteCreatorB();
        creatorB.Operation();
        // Вывод:
        // Выполнение операции B для продукта B
    }
}