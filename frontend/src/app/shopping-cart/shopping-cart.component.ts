import { Component } from '@angular/core';
import { ShoppingCardService } from '../services/shoppingCard.service';
import { ShoppingCard } from '../Model';
import { Order } from '../Model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-shopping-cart',
  imports: [CommonModule],
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent {
  shoppingCards: ShoppingCard[] = [];
  totalPrice: number = 0;
  Order: Order[] = [];

  constructor(private shoppingCardService: ShoppingCardService) { }

  ngOnInit(): void {
    const userId = parseInt(localStorage.getItem("userId") || "0");

    if (userId) {
      this.shoppingCardService.getShoppingCard(userId).subscribe({
        next: (response) => {
          console.log("Response",response);
          this.shoppingCards = Array.isArray(response) ? response : [response];
          this.calculateTotalPrice();
        },
        error: (error) => {
          console.log("API isteği hatası", error);
          this.shoppingCards = [];
        }
      });
    }
  }

  deleteProduct(productId: number): void {
    this.shoppingCardService.deleteShoppingCard(productId).subscribe({
      next: () => {
        console.log("Ürün Silindi");
        this.ngOnInit();
      },
      error: (error) => {
        console.log("Ürün Silinemedi", error);
      }
    });
  }

  completeOrder(shoppingCards: ShoppingCard[]): void {
    const orders: Order[] = shoppingCards.map(card => ({
      id: 0,
      price: card.price,
      userId: card.userId,
      productId: card.productId,
      userName: card.userName,
      productName: card.productName,
      imageUrl: card.imageUrl,
      quantity: card.quantity,
      orderNumber: "",
    }));

    localStorage.setItem('orders', JSON.stringify(orders));
    console.log("Siparişler yerel depolamaya kaydedildi:", orders);

    this.shoppingCardService.saveOrder(orders).subscribe({
      next: (response) => {
        console.log("Sipariş Başarıyla Oluşturuldu :)", response);
        alert("Sipariş başarıyla oluşturuldu!");

        localStorage.removeItem('orders');

        this.ngOnInit();
      },
      error: (error) => {
        console.log("Siparişte oluşturulamadı", error);
        alert("Sipariş oluşturulamadı: " + error.message);
      }
    });
  }


  increaseQuantity(card: ShoppingCard): void {
    card.quantity++;
    this.calculateTotalPrice();
  }

  decreaseQuantity(card: ShoppingCard): void {
    if (card.quantity > 1) {
      card.quantity--;
      this.calculateTotalPrice();
    }
  }

  calculateTotalPrice(): void {
    let total = 0;
    for (let card of this.shoppingCards) {
      total += card.price * card.quantity;
    }
    this.totalPrice = total;
  }
}
