import {
  ComponentFactory,
  ComponentFactoryResolver,
  ComponentRef,
  Injectable,
  OnDestroy,
  ViewContainerRef,
} from '@angular/core';
import { MaskLoadComponent } from './mask-load.component';

@Injectable({
  providedIn: 'root',
})
export class MaskLoadService implements OnDestroy {
  constructor(private componentFactoryResolver: ComponentFactoryResolver) {}

  private componentInstance?: ComponentRef<MaskLoadComponent>;
  private factory?: ComponentFactory<MaskLoadComponent>;
  private showed: boolean = false;

  ngOnDestroy(): void {
    this.componentInstance?.destroy();
  }

  show(viewContainerRef: ViewContainerRef) {
    if (!this.showed) {
      if (!this.factory) {
        this.factory =
          this.componentFactoryResolver.resolveComponentFactory<MaskLoadComponent>(
            MaskLoadComponent
          );
      }
      this.componentInstance =
        viewContainerRef.createComponent<MaskLoadComponent>(this.factory);

      const loaderComponentElement =
        this.componentInstance.location.nativeElement;
      const parentNode = loaderComponentElement.previousSibling;
      parentNode?.insertBefore(loaderComponentElement, parentNode.firstChild);
      this.showed = true;
    }
  }

  showInEl(viewContainerRef: ViewContainerRef, el: Element) {
    if (!this.showed) {
      if (!this.factory) {
        this.factory =
          this.componentFactoryResolver.resolveComponentFactory<MaskLoadComponent>(
            MaskLoadComponent
          );
      }
      this.componentInstance =
        viewContainerRef.createComponent<MaskLoadComponent>(this.factory);

      const loaderComponentElement =
        this.componentInstance.location.nativeElement;
      // const parentNode = loaderComponentElement.previousSibling;
      el?.insertBefore(loaderComponentElement, el.firstChild);
      this.showed = true;
    }
  }

  hide() {
    this.showed = false;
    this.componentInstance?.destroy();
  }
}
