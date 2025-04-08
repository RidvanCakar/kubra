import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';

bootstrapApplication(AppComponent, {
  ...appConfig,
  providers: [
    provideHttpClient(), // HTTP Client'ı ekliyoruz
    provideRouter(routes), // Rotaları ekliyoruz
    ...(appConfig.providers || []) // appConfig içindeki diğer provider'ları koruyoruz
  ]
}).catch((err) => console.error(err));
