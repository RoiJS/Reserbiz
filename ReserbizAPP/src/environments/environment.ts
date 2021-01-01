// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  // whitelistedDomains: ['localhost:5000'],
  // reserbizAPIEndPoint: 'http://localhost:5000/api',

  // FOR TESTING USING EMULATOR
  whitelistedDomains: ['10.0.2.2:9152'],
  reserbizAPIEndPoint: 'http://10.0.2.2:9152/api',

  // (1) FOR DEPLOYED VERSION - HOTSPOT VINSANITY
  // whitelistedDomains: ['192.168.43.250:3000'],
  // reserbizAPIEndPoint: 'http://192.168.43.250:3000/api',

  // (2) FOR DEPLOYED VERSION
  // whitelistedDomains: ['192.168.254.101:3000'],
  // reserbizAPIEndPoint: 'http://192.168.254.101:3000/api',

  // (3) FOR DEPLOYED VERSION
  // whitelistedDomains: ['172.17.219.97:3000'],
  // reserbizAPIEndPoint: 'http://172.17.219.97:3000/api',

  // This is customer's database hashed string
  appSecretToken: '3be4fbf7c0a93771b0b0a8c327f7dc7a7aa66ac4',
};

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
