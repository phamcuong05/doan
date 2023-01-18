import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LOCALE_ID, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { DialogModule, WindowModule } from '@progress/kendo-angular-dialog';
import { GridModule } from '@progress/kendo-angular-grid';
import { InputsModule, SwitchModule } from '@progress/kendo-angular-inputs';
import { IntlModule } from '@progress/kendo-angular-intl';
import '@progress/kendo-angular-intl/locales/en/all';
import '@progress/kendo-angular-intl/locales/vi/all';
import { NotificationModule } from '@progress/kendo-angular-notification';
import { TooltipModule } from '@progress/kendo-angular-tooltip';
import { NgProgressModule } from 'ngx-progressbar';
import { NgProgressHttpModule } from 'ngx-progressbar/http';
import { NgProgressRouterModule } from 'ngx-progressbar/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { Constaints } from './common/constaints';
import { FtsDialogModule } from './controls/fts-dialog/fts-dialog.module';
import { FtsNumerictextboxModule } from './controls/fts-numerictextbox/fts-numerictextbox.module';
import { MaskLoadModule } from './controls/mask-load/mask-load.module';
import { NationalLanguageModule } from './controls/national-language/national-language.module';
import { NavigationComponent } from './controls/navigation/navigation.component';
import { AuthInterceptor } from './interceptor/auth.interceptor';
import { JsonInterceptor } from './interceptor/parseJson.interceptor';
import { AppEffects } from './model/app/effec';
import { authReducer } from './model/login/reducer';
@NgModule({
  declarations: [AppComponent, NavigationComponent],
  imports: [
    BrowserModule,
    CommonModule,
    AppRoutingModule,
    HttpClientModule,
    StoreModule.forRoot({}),
    StoreModule.forFeature(Constaints.Selector.AUTH, authReducer),
    EffectsModule.forRoot([AppEffects]),
    NgProgressModule,
    NgProgressHttpModule,
    NgProgressRouterModule,
    MaskLoadModule,
    NationalLanguageModule,
    GridModule,
    BrowserAnimationsModule,
    WindowModule,
    DialogModule,
    FtsDialogModule,
    NotificationModule,
    IntlModule,
    ButtonsModule,
    FtsNumerictextboxModule,
    FormsModule,
    TooltipModule,
    SwitchModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JsonInterceptor,
      multi: true,
    },
    {
      provide: LOCALE_ID,
      useValue: 'vi-VN',
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
