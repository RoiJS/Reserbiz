// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  whitelistedDomains: ['10.0.2.2:5000'],
  reserbizAPIEndPoint: 'http://10.0.2.2:5000/api',

  // This is customer's database hashed string
  appSecretToken: '3be4fbf7c0a93771b0b0a8c327f7dc7a7aa66ac4'
};

/*
 * In development mode, to ignore zone related error stack frames such as
 * `zone.run`, `zoneDelegate.invokeTask` for easier debugging, you can
 * import the following file, but please comment it out in production mode
 * because it will have performance impact when throw error
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
