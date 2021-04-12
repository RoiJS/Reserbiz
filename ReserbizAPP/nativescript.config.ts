import { NativeScriptConfig } from '@nativescript/core';

export default {
  id: 'org.Reserbiz.ReserbizApp',
  appResourcesPath: 'App_Resources',
  android: {
    v8Flags: '--expose_gc',
    markingMode: 'none',
  },
  appPath: 'src',
} as NativeScriptConfig;
