import { Component, OnInit } from '@angular/core';
import { Product } from '../Model';
import { ProductService } from '../services/product.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ShoppingCardService } from '../services/shoppingCard.service';

@Component({
  selector: 'app-detail',
  imports: [CommonModule],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.css'
})
export class DetailComponent implements OnInit {

  productId: number | undefined;
  product: Product | undefined;

  constructor(private route: ActivatedRoute, private productService: ProductService, private shoppingCardService: ShoppingCardService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      console.log("Tüm Parametreler:", params.keys);
      const id = params.get('productId');

      this.productId = id ? +id : undefined;
      console.log('Dönüştürülen Ürün ID:', this.productId);

      console.log("satıcı:", this.product);

      if (this.productId) {
        this.productService.getProduct(this.productId).subscribe({
          next: (response) => {
            console.log("Ürün:", response);
            this.product = response;

            const price = response.id;
            const username = response.userName;
            const productName = response.name;
            const productId = response.id;
            const imageUrl=response.imageUrl;

            localStorage.setItem("price", price.toString());
            localStorage.setItem("username", username);
            localStorage.setItem("productName", productName);
            localStorage.setItem("productId", productId.toString());
            localStorage.setItem("imageUrl",imageUrl);

          },
          error: (error) => {
            console.log("API isteği hatası:", error);
          }
        }
        );
      }
    });
  }

  addShoppingCard() {
    const price = parseInt(localStorage.getItem("price") || "0");
    const userId = parseInt(localStorage.getItem("userId") || "0");
    const productId = parseInt(localStorage.getItem("productId") || "0");
    const imageUrl=(localStorage.getItem("imageUrl") || "0");
    const quantity=1;

    if (!price || !userId || !productId) {
      console.error("Eksik veri var! Lütfen kontrol edin.");
      return;
    }

    const shoppingCard = {
      price,
      userId,
      productId,
      imageUrl,
      quantity,
    };

    this.shoppingCardService.saveShoppingCard(shoppingCard).subscribe({
      next: (response) => {
        console.log("Seppette Kayıtlı Ürün Bilgileri", response);
      }
    })
  }
}
