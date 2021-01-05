import { Component, OnInit } from '@angular/core';

import { BaseWidgetComponent } from '@src/app/shared/component/base-widget.component';

import { SpaceService } from '@src/app/_services/space.service';

@Component({
  selector: 'ns-available-spaces-widget',
  templateUrl: './available-spaces-widget.component.html',
  styleUrls: ['./available-spaces-widget.component.scss'],
})
export class AvailableSpacesWidgetComponent
  extends BaseWidgetComponent
  implements OnInit {
  constructor(private spaceService: SpaceService) {
    super();
  }

  ngOnInit() {
    (async () => {
      this._isBusy = true;
      this._entityCount = await this.spaceService.getAvailableSpacesCount();
      this._isBusy = false;
    })();
  }
}
