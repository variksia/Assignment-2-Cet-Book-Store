using CetStudentBook.Data;
using CetStudentBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CetStudentBook.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }

        // GET: /Cart
        public IActionResult Index()
        {
            var userId = GetUserId();
            var cartItems = _context.CartItems
                .Include(ci => ci.Book)
                .Where(ci => ci.UserId == userId)
                .ToList();

            ViewBag.TotalPrice = cartItems.Sum(ci => ci.Book!.Price * ci.Quantity);
            return View(cartItems);
        }

        // POST: /Cart/AddToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int bookId)
        {
            var userId = GetUserId();

            // Check if item already in cart
            var existingItem = _context.CartItems
                .FirstOrDefault(ci => ci.UserId == userId && ci.BookId == bookId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                var cartItem = new CartItem
                {
                    UserId = userId,
                    BookId = bookId,
                    Quantity = 1
                };
                _context.CartItems.Add(cartItem);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            var userId = GetUserId();
            var cartItem = _context.CartItems
                .FirstOrDefault(ci => ci.Id == cartItemId && ci.UserId == userId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateQuantity(int cartItemId, int quantity)
        {
            var userId = GetUserId();
            var cartItem = _context.CartItems
                .FirstOrDefault(ci => ci.Id == cartItemId && ci.UserId == userId);

            if (cartItem != null)
            {
                if (quantity <= 0)
                {
                    _context.CartItems.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = quantity;
                }
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Purchase()
        {
            var userId = GetUserId();
            var cartItems = _context.CartItems
                .Include(ci => ci.Book)
                .Where(ci => ci.UserId == userId)
                .ToList();

            if (!cartItems.Any())
            {
                TempData["Error"] = "Your cart is empty!";
                return RedirectToAction("Index");
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalPrice = cartItems.Sum(ci => ci.Book!.Price * ci.Quantity)
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var cartItem in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    BookId = cartItem.BookId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Book!.Price
                };
                _context.OrderItems.Add(orderItem);
            }

            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();

            TempData["Success"] = "Purchase completed successfully!";
            return RedirectToAction("Index", "Orders");
        }
    }
}
