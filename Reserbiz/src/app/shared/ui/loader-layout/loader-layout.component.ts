import { Component, OnInit, Input } from '@angular/core';
@Component({
  selector: 'ns-loader-layout',
  templateUrl: './loader-layout.component.html',
  styleUrls: ['./loader-layout.component.scss']
})
export class LoaderLayoutComponent implements OnInit {
  constructor() {}

  @Input() busy = false;

  ngOnInit() {}
}
