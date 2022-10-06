import { environment as devEnvironment } from './environment.dev';
import { environment as prodEnvironment } from './environment.prod';

declare const env: any;

export const environment: typeof prodEnvironment = (() => {
  // if (env?.production) {
  //   return prodEnvironment;
  // }

  return <typeof prodEnvironment>devEnvironment;
})();
