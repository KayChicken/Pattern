using System;
using System.Collections.Generic;


// Посетитель
interface IVisitor
{
    void VisitPizza(Pizza pizza);
    void VisitSushi(Sushi sushi);
    void VisitBurger(Burger burger);
}

// Конкретный посетитель для расчета скидок
class DiscountVisitor : IVisitor
{
    private decimal discount;

    public void VisitPizza(Pizza pizza)
    {
        discount += 0.1m; // 10% скидка на пиццу
    }

    public void VisitSushi(Sushi sushi)
    {
        discount += 0.05m; // 5% скидка на суши
    }

    public void VisitBurger(Burger burger)
    {
        discount += 0.15m; // 15% скидка на бургеры
    }

    public decimal GetTotalDiscount()
    {
        return discount;
    }
}

// Абстрактный класс еды
abstract class Food
{
    public string Name { get; protected set; }
    public decimal Price { get; protected set; }

    public abstract void Accept(IVisitor visitor);
}

// Конкретный класс пиццы
class Pizza : Food
{
    public Pizza()
    {
        Name = "Pizza";
        Price = 480.0m;
    }

    public override void Accept(IVisitor visitor)
    {
        visitor.VisitPizza(this);
    }
}

// Конкретный класс суши
class Sushi : Food
{
    public Sushi()
    {
        Name = "Sushi";
        Price = 450.0m;
    }

    public override void Accept(IVisitor visitor)
    {
        visitor.VisitSushi(this);
    }
}

// Конкретный класс бургеров
class Burger : Food
{
    public Burger()
    {
        Name = "Burger";
        Price = 280.0m;
    }

    public override void Accept(IVisitor visitor)
    {
        visitor.VisitBurger(this);
    }
}

// Абстрактная фабрика еды
abstract class FoodFactory
{
    public abstract Food CreateFood();
}

// Конкретная фабрика для создания пиццы
class PizzaFactory : FoodFactory
{
    public override Food CreateFood()
    {
        return new Pizza();
    }
}

// Конкретная фабрика для создания суши
class SushiFactory : FoodFactory
{
    public override Food CreateFood()
    {
        return new Sushi();
    }
}

// Конкретная фабрика для создания бургеров
class BurgerFactory : FoodFactory
{
    public override Food CreateFood()
    {
        return new Burger();
    }
}

// Класс заказа
class Order
{
    private List<Food> foods = new List<Food>();

    public void AddFood(Food food)
    {
        foods.Add(food);
    }

    public decimal CalculateTotalPrice()
    {
        decimal totalPrice = 0.0m;
        foreach (var food in foods)
        {
            totalPrice += food.Price;
        }
        return totalPrice;
    }
}

// Класс оплаты
class Payment
{
    public void MakePayment(Order order, decimal discount)
    {
        decimal total = order.CalculateTotalPrice();
        total -= (total * discount);
        decimal formattedNumber = Math.Round(total, 2);
        Console.WriteLine($"Total Price With Discout: {formattedNumber}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создание объектов через фабричный метод
        FoodFactory pizzaFactory = new PizzaFactory();
        FoodFactory sushiFactory = new SushiFactory();
        FoodFactory burgerFactory = new BurgerFactory();

        Food pizza = pizzaFactory.CreateFood();
        Food sushi = sushiFactory.CreateFood();
        Food burger = burgerFactory.CreateFood();

        // Добавление еды в заказ
        Order order = new Order();
        order.AddFood(pizza);
        order.AddFood(sushi);
        order.AddFood(burger);

        // Расчет скидок
        DiscountVisitor discountVisitor = new DiscountVisitor();
        pizza.Accept(discountVisitor);
        sushi.Accept(discountVisitor);
        burger.Accept(discountVisitor);
        decimal totalDiscount = discountVisitor.GetTotalDiscount();

        // Печать информации о заказе и скидках
        Console.WriteLine("Order Details:");
        Console.WriteLine($"Total price: {order.CalculateTotalPrice()}");
        Console.WriteLine($"Discount: {totalDiscount}");

        // Оплата заказа
        Payment payment = new Payment();
        payment.MakePayment(order,totalDiscount);

        Console.ReadLine();
    }
}
