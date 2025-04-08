export class Model {
  products: Array<Product>;
  users:Array<User>;
  shoppingCard:Array<ShoppingCard>;
  orders:Array<Order>;


  constructor() {
    this.products = [];
    this.users=[];
    this.shoppingCard=[];
    this.orders=[];
  }
}

export interface Product {
  id:number,
  userId:number;
  categoryName: any;
  userName:any;
  name: string;
  price: number;
  imageUrl: string;
  categoryId: number;
  stock: number;
}

export interface User {
  id:number;
  username:string;
  email:string;
  password:string;
}

export interface ShoppingCard{
  id:number,
  price:number;
  userId:number;
  productId:number;
  userName:string,
  productName:string,
  imageUrl:string,
  quantity:number,
}

export interface Order{
  id:number,
  price:number;
  userId:number;
  productId:number;
  userName:string,
  productName:string,
  imageUrl:string,
  quantity:number,
  orderNumber:string,
}


