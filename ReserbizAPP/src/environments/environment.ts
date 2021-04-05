// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  // reserbizAPIEndPoint: 'http://localhost:5000/api',

  // FOR TESTING USING EMULATOR
  reserbizAPIEndPoint: 'http://10.0.2.2:9152/api',
  reserbizAPIEndPointWebsocket: 'http://10.0.2.2:9152/websocket',

  // (1) FOR DEPLOYED VERSION - HOTSPOT VINSANITY
  // reserbizAPIEndPoint: 'http://192.168.43.250:3000/api',
  // reserbizAPIEndPointWebsocket: 'http://192.168.43.250:3000/websocket',

  // (3) FOR DEPLOYED VERSION - WIFI GLOBE WIFI
  // reserbizAPIEndPoint: 'http://192.168.254.101:3000/api',
  // reserbizAPIEndPointWebsocket: 'http://192.168.254.101:3000/websocket',
};

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
