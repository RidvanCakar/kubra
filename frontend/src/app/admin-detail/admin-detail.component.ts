import { Component, OnInit } from '@angular/core';
import { ProductService } from '../services/product.service';
import { Product } from '../Model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-detail',
  imports: [FormsModule, CommonModule],
  templateUrl: './admin-detail.component.html',
  styleUrl: './admin-detail.component.css'
})
export class AdminDetailComponent implements OnInit {

  products: Product[] = [];
  selectedProduct: Product | null = null;

  constructor(private productService: ProductService,private router:Router) { }

  get username(): string {
    return localStorage.getItem("username") || "";
  }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    const userId = localStorage.getItem("userId");

    if (!userId) {
      console.error("User ID bulunamadı!");
      return;
    }

    this.productService.getAdminProduct(parseInt(userId)).subscribe({
      next: (products) => {
        this.products = products;
      },
      error: (error) => {
        console.error('Ürünler yüklenirken hata:', error);
      }
    });
  }


  addProduct(name: string, price: string, categoryId: string, imageUrl: string, stock: string): void {
    const adminId = localStorage.getItem("userId");

    if (!adminId) {
      console.error("Admin ID bulunamadı!");
      return;
    }

    const newProduct: Product = {
      id: 0,
      userId: parseInt(adminId),
      userName: localStorage.getItem("username") || undefined,
      name: name,
      price: parseFloat(price),
      imageUrl: imageUrl,
      categoryId: parseInt(categoryId),
      stock: parseInt(stock),
      categoryName: undefined
    };

    this.productService.saveProduct(newProduct).subscribe({
      next: (response) => {
        console.log("Ürün Yükleme Başarılı:", response);
        this.loadProducts();
      },
      error: (error) => {
        console.error('Yükleme Hatası:', error);
        if (error.error && error.error.errors) {
          console.log(error.error.errors);
        }
      }
    });
  }

  deleteProduct(product: Product): void {
    if (confirm('Ürünü silmek istediğinizden emin misiniz?')) {
      this.productService.deleteProduct(product.id).subscribe({
        next: () => {
          console.log("Ürün Silindi");
          if (this.products) {
            this.products = this.products.filter(p => p.id !== product.id);
          }
        },
        error: (error) => {
          console.error('Ürün silme hatası:', error);
        }
      });
    }
  }

  onSelectProduct(product: Product): void {
    this.selectedProduct = { ...product };
  }

  editProduct(product: Product): void {
    this.productService.updateProduct(product).subscribe({
      next: (response) => {
        console.log('Ürün Güncellendi:', response);
        this.selectedProduct = null;
        this.loadProducts();
      },
      error: (error) => {
        console.error('Ürün güncelleme hatası:', error);
      }
    });
  }

  logout() {
    localStorage.removeItem("token");
    this.router.navigate(['/login']);
  }

  

}
