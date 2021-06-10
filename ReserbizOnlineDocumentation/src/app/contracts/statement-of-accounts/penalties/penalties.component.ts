import {
  AfterViewChecked,
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { UIService } from 'src/app/services/ui.service';
import { SharedComponent } from 'src/app/shared/components/shared/shared.component';

@Component({
  selector: 'app-penalties',
  templateUrl: './penalties.component.html',
  styleUrls: ['./penalties.component.scss'],
})
export class PenaltiesComponent
  extends SharedComponent
  implements OnInit, OnDestroy, AfterViewInit, AfterViewChecked
{
  constructor(
    protected activatedRoute: ActivatedRoute,
    protected uiService: UIService
  ) {
    super(uiService);
    this.activatedRoute = activatedRoute;
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  ngOnDestroy(): void {
    super.ngOnDestroy();
  }

  ngAfterViewInit(): void {
    super.ngAfterViewInit();
  }

  ngAfterViewChecked(): void {
    super.ngAfterViewChecked();
  }
}
