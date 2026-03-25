OrderFacade store = new OrderFacade();

store.PlaceOrder(
    item:       "Wireless Headphones",
    cardNumber: "4111111111111234",
    amount:     79.99,
    address:    "42 Maple Street, London",
    email:      "customer@email.com"
);


// Subsystem 1: checks whether the item is available
public class InventoryService
{
    public bool CheckStock(string item)
    {
        Console.WriteLine($"Inventory: Checking stock for {item}.");
        return true;
    }
}

// Subsystem 2: handles the payment
public class PaymentService
{
    public bool ProcessPayment(string cardNumber, double amount)
    {
        Console.WriteLine($"Payment: Charging £{amount:F2} to card ending {cardNumber[^4..]}.");
        return true;
    }
}

// Subsystem 3: generates a shipping label
public class ShippingService
{
    public string GenerateLabel(string item, string address)
    {
        Console.WriteLine($"Shipping: Generating label for {item} to {address}.");
        return "TRACK-29384";
    }
}

// Subsystem 4: sends the confirmation email
public class EmailService
{
    public void SendConfirmation(string email, string trackingCode)
    {
        Console.WriteLine($"Email: Confirmation sent to {email}. Tracking code: {trackingCode}.");
    }
}

// The Facade, one method hides all four subsystems
public class OrderFacade
{
    private readonly InventoryService _inventory = new();
    private readonly PaymentService   _payment   = new();
    private readonly ShippingService  _shipping  = new();
    private readonly EmailService     _email     = new();

    public void PlaceOrder(string item, string cardNumber, double amount, string address, string email)
    {
        Console.WriteLine("=== Placing Order ===");

        if (!_inventory.CheckStock(item))
        {
            Console.WriteLine("Order failed: item out of stock.");
            return;
        }

        if (!_payment.ProcessPayment(cardNumber, amount))
        {
            Console.WriteLine("Order failed: payment declined.");
            return;
        }

        string trackingCode = _shipping.GenerateLabel(item, address);
        _email.SendConfirmation(email, trackingCode);

        Console.WriteLine($"\nOrder complete. Your tracking code is {trackingCode}.");
    }
}
