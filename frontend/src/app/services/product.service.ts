import { Injectable } from '@angular/core';
import { Product } from '../Model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  baseUrl: string = "http://localhost:5248/";

  constructor(private http: HttpClient) { }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl + "api/Product/getproducts");
  }

  saveProduct(product: Product): Observable<Product> {
      return this.http.post<Product>(this.baseUrl + "api/Product/AddProducts", product);
  }

  updateProduct(product: Product): Observable<Product> {
    return this.http.put<Product>(this.baseUrl+`api/Product/PutProduct/${product.id}`, product);
  }

  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(this.baseUrl + `api/Product/deleteproduct/${id}`);
  }

  getCategoryProduct(categoryName:string):Observable<Product[]>{
    return this.http.get<Product[]>(this.baseUrl+`api/Category/GetProductsCategory/${categoryName}`);
  }

  getProduct(id:number):Observable<Product>{
    return this.http.get<Product>(this.baseUrl+`api/Product/getproduct/${id}`);
  }

  getAdminProduct(userId: number): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl + `api/Product/GetProductAdmin/${userId}`);
  }

 

}

