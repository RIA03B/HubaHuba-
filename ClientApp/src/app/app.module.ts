import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { AccessDeniedComponent } from './access-denied/access-denied.component';
import { NotFoundComponent } from './not-found/not-found.component';
import {ButtonModule} from 'primeng/button';
import {InputTextModule} from 'primeng/inputtext';
import {ColorPickerModule} from 'primeng/colorpicker';
import {TableModule} from 'primeng/table';
import {CardModule} from 'primeng/card';
import {ConfirmDialogModule} from 'primeng/confirmdialog';
import {ConfirmationService, MessageService} from 'primeng/api';
import {ConfirmPopupModule} from 'primeng/confirmpopup';
import {DialogModule} from 'primeng/dialog';
import {MessagesModule} from 'primeng/messages';
import {MessageModule} from 'primeng/message';
import {ToastModule} from 'primeng/toast';
import {GalleriaModule} from 'primeng/galleria';
import {CarouselModule} from 'primeng/carousel';
import {ProgressSpinnerModule} from 'primeng/progressspinner';
import {DropdownModule} from 'primeng/dropdown';
import {CheckboxModule} from 'primeng/checkbox';
import {RadioButtonModule} from 'primeng/radiobutton';
import {MatCardModule} from '@angular/material/card';
import {InputTextareaModule} from 'primeng/inputtextarea';
import {AccordionModule} from 'primeng/accordion';
import {InputMaskModule} from 'primeng/inputmask';
import {InputNumberModule} from 'primeng/inputnumber';
import {FileUploadModule} from 'primeng/fileupload';
import { EmojiModule } from '@ctrl/ngx-emoji-mart/ngx-emoji';
import { AdviseComponent } from './advise/advise.component';
import { EventsComponent } from './events/events.component';
import { WardrobeComponent } from './wardrobe/wardrobe.component';
import { GarmentsComponent } from './garments/garments.component';
import { ProfileComponent } from './profile/profile.component';
import { OrdersComponent } from './orders/orders.component';

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'home', component: HomeComponent },
  { path: 'advise', component: AdviseComponent },
  { path: 'events', component: EventsComponent },
  { path: 'wardrobe', component: WardrobeComponent },
  { path: 'garments', component: GarmentsComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'orders', component: OrdersComponent },
  {path: '**', component: NotFoundComponent}
];

@NgModule({
  
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    ButtonModule,
    TableModule,
    InputTextareaModule,
    EmojiModule,
    FileUploadModule,
    BrowserAnimationsModule,
    InputTextModule,
    DropdownModule,
    InputNumberModule,
    InputMaskModule,
    CheckboxModule,
    CarouselModule,
    CardModule,
    AccordionModule,
    RadioButtonModule,
    ColorPickerModule,
    GalleriaModule,
    ConfirmDialogModule,
    MatCardModule,
    MessagesModule,
    ToastModule,
    ProgressSpinnerModule,
    DialogModule,
    MessageModule,
    ConfirmPopupModule,
    RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })
  ],
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AccessDeniedComponent,
    NotFoundComponent,
    AdviseComponent,
    EventsComponent,
    WardrobeComponent,
    GarmentsComponent,
    ProfileComponent,
    OrdersComponent,

  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true } , MessageService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
