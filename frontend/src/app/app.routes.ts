import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AppComponent } from './app.component';
import { ProductService } from './services/product.service';
import { CategoryComponent } from './category/category.component';
import { DetailComponent } from './product-detail/product-detail.component';
import { AuthComponent } from './auth/auth.component';
import { SellerAuthComponent } from './seller-auth/seller-auth.component';
import { SuperAdminDetailComponent } from './super-admin-detail/super-admin-detail.component';
import { AdminDetailComponent } from './admin-detail/admin-detail.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { adminGuard } from './guards/admin.guard';
import { userGuard } from './guards/user.guard';
import { süperAdminGuard } from './guards/süperadmin.guard';
import { ShoppingCartComponent } from './shopping-cart/shopping-cart.component';

export const routes: Routes = [
  
  { path: "", component: HomeComponent },
  { path: "auth", component: AuthComponent },
  { path: "sellerAuth", component: SellerAuthComponent },
  { path: "shoppingCart", component: ShoppingCartComponent },
  { path: "süperAdmin/:userId", component: SuperAdminDetailComponent, canActivate: [süperAdminGuard] },
  { path: "admin/:userId", component: AdminDetailComponent, canActivate: [adminGuard] },
  { path: "user/:userId", component: UserDetailComponent, canActivate: [userGuard] },
  { path: "product/:productId", component: DetailComponent },
  { path: ":categoryName", component: CategoryComponent },

];
