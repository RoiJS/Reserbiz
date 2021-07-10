import {
  AfterViewChecked,
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Subscription } from 'rxjs';

import { UIService } from 'src/app/services/ui.service';
import { IField } from 'src/app/_interfaces/ifield';

@Component({
  selector: 'app-shared',
  templateUrl: './shared.component.html',
})
export class SharedComponent
  implements OnInit, OnDestroy, AfterViewInit, AfterViewChecked
{
  protected _isHandset = false;
  protected _fragment: string | null = '';

  protected isHandSetSubscription: Subscription | undefined;
  protected fragmentSubscription: Subscription | undefined;

  protected activatedRoute: ActivatedRoute | null = null;

  displayedColumns: string[] = ['name', 'datatype', 'definition'];
  displayedColumns2: string[] = [
    'name',
    'defaultValue',
    'datatype',
    'definition',
  ];
  dataSource: IField[] = [];
  dataSource2: IField[] = [];
  dataSource3: IField[] = [];
  dataSource4: IField[] = [];
  dataSource5: IField[] = [];
  dataSource6: IField[] = [];
  dataSource7: IField[] = [];
  dataSource8: IField[] = [];
  dataSource9: IField[] = [];

  constructor(protected uiService: UIService) {
    this._fragment = '';
  }

  ngOnInit(): void {
    this.fragmentSubscription = this.activatedRoute?.fragment.subscribe(
      (fragment: string | null) => {
        this._fragment = fragment;
      }
    );
  }

  ngOnDestroy(): void {
    if (this.isHandSetSubscription) {
      this.isHandSetSubscription?.unsubscribe();
    }

    if (this.fragmentSubscription) {
      this.fragmentSubscription?.unsubscribe();
    }
  }

  ngAfterViewInit(): void {
    this.isHandSetSubscription = this.uiService.isHandset.subscribe(
      (result: boolean) => {
        this._isHandset = result;
      }
    );
  }

  ngAfterViewChecked(): void {
    try {
      document.querySelector('#' + this._fragment)?.scrollIntoView();
      // window.location.hash = '';
    } catch (e) {}
  }

  get isHandset(): boolean {
    return this._isHandset;
  }
}
