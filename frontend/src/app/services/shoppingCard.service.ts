import { Injectable } from "@angular/core";
import { Order, ShoppingCard } from "../Model";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { REACTIVE_NODE } from "@angular/core/primitives/signals";
import { ReactiveFormsModule } from "@angular/forms";

@Injectable({
  providedIn: "root"
})

export class ShoppingCardService {

  baseUrl: string = "http://localhost:5248/";

  constructor(private http: HttpClient) { }

  getShoppingCards(): Observable<ShoppingCard[]> {
    return this.http.get<ShoppingCard[]>(this.baseUrl + "api/ShoppingCard/GetShoppingCards");
  }

  getShoppingCard(id: number): Observable<ShoppingCard> {
    return this.http.get<ShoppingCard>(this.baseUrl + `api/ShoppingCard/GetShoppingCard/${id}`);
  }

  saveShoppingCard(shoppingCard: any): Observable<any> {
    return this.http.post<ShoppingCard>(this.baseUrl + "api/ShoppingCard/PostShoppingCard", shoppingCard);
  }

  deleteShoppingCard(id: number): Observable<ShoppingCard> {

    return this.http.delete<ShoppingCard>(this.baseUrl + `api/ShoppingCard/DeleteProduct/${id}`);
  }


  saveOrder(orders: Order[]): Observable<any> {
    return this.http.post<Order[]>(`${this.baseUrl}api/ShoppingCard/PostOrder`, orders);
  }
}
