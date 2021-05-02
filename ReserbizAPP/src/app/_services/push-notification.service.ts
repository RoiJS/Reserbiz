import { Injectable } from '@angular/core';

import { firebase } from '@nativescript/firebase';

import { StorageService } from './storage.service';

@Injectable({ providedIn: 'root' })
export class PushNotificationService {
  constructor(private storageService: StorageService) {}

  subscribe() {
    if (this.storageService.hasKey('app-secret-token')) {
      const appSecretToken = this.storageService.getString('app-secret-token');
      firebase
        .subscribeToTopic(appSecretToken)
        .then(() =>
          console.log(
            `Subscribing to notification topic ${appSecretToken} has been established!`
          )
        );
    }
  }

  //   unsubscribe(clientDbHashName: string) {
  //     firebase
  //       .unsubscribeFromTopic(clientDbHashName)
  //       .then(() =>
  //         console.log(
  //           `Unsubscribing to notification topic ${clientDbHashName} has been disconnected!`
  //         )
  //       );
  //   }
}
