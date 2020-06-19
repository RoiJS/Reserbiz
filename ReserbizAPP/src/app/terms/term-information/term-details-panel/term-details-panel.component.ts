import { Component, OnInit, Input } from '@angular/core';
import { RouterExtensions } from 'nativescript-angular/router';

import { Term } from '@src/app/_models/term.model';

@Component({
  selector: 'ns-term-details-panel',
  templateUrl: './term-details-panel.component.html',
  styleUrls: ['./term-details-panel.component.scss'],
})
export class TermDetailsPanelComponent implements OnInit {
  @Input() currentTerm: Term;
  constructor(private router: RouterExtensions) {}

  ngOnInit() {}

  onNavigateToEditPage() {
    this.router.navigate([`/terms/${this.currentTerm.id}/edit`], {
      transition: {
        name: 'slideLeft',
      },
    });
  }
}
