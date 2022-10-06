import { Injectable, NgZone } from "@angular/core";

import { RouterExtensions } from "@nativescript/angular";

import { firebase } from "@nativescript/firebase";
import { LocalNotifications } from "@nativescript/local-notifications";

import { BehaviorSubject } from "rxjs";

import { StorageService } from "./storage.service";

@Injectable({ providedIn: "root" })
export class PushNotificationService {
  private _navigateToUrl = new BehaviorSubject<boolean>(false);
  private _url = new BehaviorSubject<string>("");

  constructor(
    private router: RouterExtensions,
    private zone: NgZone,
    private storageService: StorageService
  ) {}

  subscribe() {
    if (this.storageService.hasKey("app-secret-token")) {
      const appSecretToken = this.storageService.getString("app-secret-token");
      this.storageService.storeString(
        "current-notification-topic",
        appSecretToken
      );
      firebase
        .subscribeToTopic(appSecretToken)
        .then(() =>
          console.log(
            `Subscribing to notification topic ${appSecretToken} has been established!`
          )
        );

      firebase.addOnMessageReceivedCallback((message: firebase.Message) => {
        this.zone.run(() => {
          LocalNotifications.schedule([
            {
              title: message.data.title,
              body: message.data.body,
              bigTextStyle: true,
              at: new Date(new Date().getTime() + 5 * 1000),
              payload: message.data,
            },
          ]);
        });
      });

      LocalNotifications.addOnMessageReceivedCallback((data) => {
        this.zone.run(() => {
          if (!data.payload.foreground) {
            this._navigateToUrl.next(true);
            this._url.next(data.payload.url);
            this.router.navigate([data.payload.url]);
          }
        });
      });
    }
  }

  unsubscribe() {
    const topicNotification = this.storageService.getString(
      "current-notification-topic"
    );

    if (topicNotification) {
      firebase
        .unsubscribeFromTopic(topicNotification)
        .then(() =>
          console.log(
            `Unsubscribing to notification topic ${topicNotification}!`
          )
        );
    }
  }

  get navigateToUrl(): BehaviorSubject<boolean> {
    return this._navigateToUrl;
  }

  get url(): BehaviorSubject<string> {
    return this._url;
  }
}
