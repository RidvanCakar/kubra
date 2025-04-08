import { Component, OnInit } from '@angular/core';
import { ProductService } from '../services/product.service';
import { Product } from '../Model';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-category',
  imports: [CommonModule,RouterModule],
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  categoryName: string | undefined;
  products: Product[] = [];

  constructor(private route: ActivatedRoute, private productService: ProductService) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.categoryName = params.get('categoryName') ?? '';
      console.log('Kategori adı:', this.categoryName);

      if (this.categoryName) {
        this.productService.getCategoryProduct(this.categoryName).subscribe(
          (products) => {
            console.log('Ürünler:', products);
            this.products = products;
          },
          (error) => {
            console.error('API isteği hatası:', error);
          }
        );
      }
    });
  }


}



