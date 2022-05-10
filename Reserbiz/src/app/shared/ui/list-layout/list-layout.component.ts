import { Component, Input, OnInit } from '@angular/core';

import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'ns-list-layout',
  templateUrl: './list-layout.component.html',
  styleUrls: ['./list-layout.component.scss'],
})
export class ListLayoutComponent implements OnInit {
  @Input() hasItems = true;
  @Input() emptyTextIdentifier = '';
  @Input() emptyText = '';

  constructor(private translateService: TranslateService) {}

  ngOnInit() {}

  get emptyListText(): string {
    if (this.emptyText.length > 0) {
      return this.emptyText;
    }

    if (this.emptyTextIdentifier.length > 0) {
      return this.translateService.instant(this.emptyTextIdentifier);
    }

    return this.translateService.instant(
      'GENERAL_TEXTS.EMPTY_LIST_DEFAULT_TEXT'
    );
  }
}
