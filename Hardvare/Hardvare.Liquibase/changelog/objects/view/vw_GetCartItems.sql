SELECT ci.CartId, ci.ProductId, p.name, ci.quantity, p.price, ci.quantity * p.price AS total
FROM CartItem ci
JOIN Product p on p.Id = ci.ProductId