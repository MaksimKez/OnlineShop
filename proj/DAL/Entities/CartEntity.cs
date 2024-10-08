﻿namespace DAL.Entities;

public class CartEntity
{
    public int Id { get; set; }
    public double Discount { get; set; }
    public ICollection<CartItemEntity> CartItems { get; set; }
    public double TotalPrice { get; set; }
}