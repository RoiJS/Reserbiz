module.exports = function (config) {
  /**
   * NOTE: You can only activate one specific-platform karma configuration options
   * at a time when running unit tests.
   */

  /**
   * Karma Configuration for Angular Mobile Application
   */
  const options = {
    // base path that will be used to resolve all patterns (eg. files, exclude)
    basePath: '',

    // frameworks to use
    // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
    frameworks: ['jasmine'],

    files: [
      'src/tests/**/test.tns.ts',
      'src/tests/**/*.test.tns.ts',
      // 'src/tests/**/entity-list-service.test.tns.ts',
    ],

    // list of files to exclude
    exclude: [],

    // preprocess matching files before serving them to the browser
    // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
    preprocessors: {},

    // test results reporter to use
    // possible values: 'dots', 'progress'
    // available reporters: https://npmjs.org/browse/keyword/karma-reporter
    reporters: ['progress'],

    // web server port
    port: 9876,

    // enable / disable colors in the output (reporters and logs)
    colors: true,

    // level of logging
    // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
    logLevel: config.LOG_INFO,

    // enable / disable watching file and executing tests whenever any file changes
    autoWatch: true,

    // start these browsers
    // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
    browsers: [],

    // Continuous Integration mode
    // if true, Karma captures browsers, runs the tests and exits
    singleRun: false,

    customLaunchers: {
      android: {
        base: 'NS',
        platform: 'android',
      },
      ios: {
        base: 'NS',
        platform: 'ios',
      },
      ios_simulator: {
        base: 'NS',
        platform: 'ios',
        arguments: ['--emulator'],
      },
    },
  };

  /**
   * Karma Configuration for Angular Web Application
   */
  // const options = {
  //   basePath: '',
  //   frameworks: ['jasmine', '@angular-devkit/build-angular'],
  //   plugins: [
  //     require('karma-jasmine'),
  //     require('karma-chrome-launcher'),
  //     require('karma-jasmine-html-reporter'),
  //     require('karma-coverage-istanbul-reporter'),
  //     require('@angular-devkit/build-angular/plugins/karma'),
  //   ],
  //   client: {
  //     clearContext: false, // leave Jasmine Spec Runner output visible in browser
  //   },
  //   coverageIstanbulReporter: {
  //     dir: require('path').join(__dirname, './coverage/ReserbizApp'),
  //     reports: ['html', 'lcovonly', 'text-summary'],
  //     fixWebpackSourcePaths: true,
  //   },
  //   reporters: ['progress', 'kjhtml'],
  //   port: 9876,
  //   colors: true,
  //   logLevel: config.LOG_INFO,
  //   autoWatch: true,
  //   browsers: ['Chrome'],
  //   singleRun: false,
  //   restartOnFileChange: true,
  // };

  setWebpackPreprocessor(config, options);
  setWebpack(config, options);

  config.set(options);
};

function setWebpackPreprocessor(config, options) {
  if (config && config.bundle) {
    if (!options.preprocessors) {
      options.preprocessors = {};
    }

    options.files.forEach((file) => {
      if (!options.preprocessors[file]) {
        options.preprocessors[file] = [];
      }
      options.preprocessors[file].push('webpack');
    });
  }
}

function setWebpack(config, options) {
  if (config && config.bundle) {
    const env = {};
    env[config.platform] = true;
    env.sourceMap = config.debugBrk;
    env.appPath = config.appPath;
    options.webpack = require('./webpack.config')(env);
    delete options.webpack.entry;
    delete options.webpack.output.libraryTarget;
    const invalidPluginsForUnitTesting = [
      'GenerateBundleStarterPlugin',
      'GenerateNativeScriptEntryPointsPlugin',
    ];
    options.webpack.plugins = options.webpack.plugins.filter(
      (p) => !invalidPluginsForUnitTesting.includes(p.constructor.name)
    );
  }
}
