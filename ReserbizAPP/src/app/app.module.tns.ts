import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { JwtModule, JWT_OPTIONS } from '@auth0/angular-jwt';

import { NativeScriptModule } from 'nativescript-angular/nativescript.module';
import { NativeScriptHttpClientModule } from 'nativescript-angular/http-client';
import { NativeScriptUISideDrawerModule } from 'nativescript-ui-sidedrawer/angular/side-drawer-directives';

import { AppRoutingModule } from '@src/app/app-routing.module';
import { AppComponent } from '@src/app/app.component';

import { AuthGuard } from './_guards/auth.guard';
import { ErrorInteceptorProvider } from './_services/error.interceptor.service';
import { SecretKeyInterceptorProvider } from './_services/secret-key.interceptor.service';
import { StorageService } from './_services/storage.service';

import { jwtOptionsFactory } from './_services/jwt-interceptor.service';

// Uncomment and add to NgModule imports if you need to use two-way binding
// import { NativeScriptFormsModule } from 'nativescript-angular/forms';

@NgModule({
  declarations: [AppComponent],
  imports: [
    NativeScriptModule,
    NativeScriptHttpClientModule,
    NativeScriptUISideDrawerModule,
    AppRoutingModule,
    JwtModule.forRoot({
      jwtOptionsProvider: {
        provide: JWT_OPTIONS,
        useFactory: jwtOptionsFactory,
        deps: [StorageService]
      }
    })
  ],
  providers: [AuthGuard, SecretKeyInterceptorProvider, ErrorInteceptorProvider],
  bootstrap: [AppComponent],
  schemas: [NO_ERRORS_SCHEMA]
})
export class AppModule {}
