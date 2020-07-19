import { TestBed, getTestBed } from '@angular/core/testing';
import {
  BrowserDynamicTestingModule,
  platformBrowserDynamicTesting,
} from '@angular/platform-browser-dynamic/testing';
import { NS_COMPILER_PROVIDERS } from 'nativescript-angular/platform';
import { EntityListService } from '@src/app/_services/entity-list.service';
import { Entity } from '@src/app/_models/entity.model';
import { Injector } from '@angular/core';

class TestModel extends Entity {
  testProp1: string;
  testProp2: number;
}

class TestEntityListService extends EntityListService<TestModel> {}

describe('Entity List Service Test Suite', () => {
  let testEntityListService: TestEntityListService;
  let injector: Injector;

  beforeEach((done) => {
    TestBed.resetTestEnvironment();

    TestBed.initTestEnvironment(
      BrowserDynamicTestingModule,
      platformBrowserDynamicTesting(NS_COMPILER_PROVIDERS)
    );

    TestBed.configureTestingModule({
      imports: [],
      providers: [EntityListService, TestEntityListService],
    });

    TestBed.compileComponents()
      .then(() => done())
      .catch((e) => {
        console.log(`Failed to instantiate test component with error: ${e}`);
        console.log(e.stack);
        done();
      });

    injector = getTestBed();
    testEntityListService = injector.get(TestEntityListService);
    testEntityListService.resetEntityList();
  });

  it('should add new entity', () => {
    // Arrange
    const testModel = new TestModel();
    testModel.testProp1 = 'test 1';
    testModel.testProp2 = 1;

    // Act
    testEntityListService.addNewEntity(testModel);

    // Assert
    const expectedResult: TestModel[] = [];
    const expectedTestModel1 = new TestModel();
    expectedTestModel1.id = -1;
    expectedTestModel1.testProp1 = 'test 1';
    expectedTestModel1.testProp2 = 1;
    expectedResult.push(expectedTestModel1);

    expect(testEntityListService.entityList.value).toEqual(expectedResult);
  });

  it('should update existing entity', () => {
    // Arrange
    const testModel1 = new TestModel();
    testModel1.testProp1 = 'test 1';
    testModel1.testProp2 = 1;

    const testModel2 = new TestModel();
    testModel2.testProp1 = 'test 2';
    testModel2.testProp2 = 2;

    testEntityListService.addNewEntity(testModel1);
    testEntityListService.addNewEntity(testModel2);

    // Act
    const testModel1TestUpdate = new TestModel();
    testModel1TestUpdate.id = -1;
    testModel1TestUpdate.testProp1 = 'test 1-update';
    testModel1TestUpdate.testProp2 = 1;
    testEntityListService.updateEntity(testModel1TestUpdate);

    // Assert
    const expectedResult: TestModel[] = [];

    const expectedTestModel1 = new TestModel();
    expectedTestModel1.id = -1;
    expectedTestModel1.testProp1 = 'test 1-update';
    expectedTestModel1.testProp2 = 1;

    const expectedTestModel2 = new TestModel();
    expectedTestModel2.id = -2;
    expectedTestModel2.testProp1 = 'test 2';
    expectedTestModel2.testProp2 = 2;

    expectedResult.push(expectedTestModel1);
    expectedResult.push(expectedTestModel2);

    console.log(`Actual: ${testEntityListService.entityList.value.length}`);
    console.log(`Expected: ${expectedResult.length}`);

    expect(testEntityListService.entityList.value).toEqual(expectedResult);
  });

  it('should remove existing entity', () => {
    // Arrange
    const testModel1 = new TestModel();
    testModel1.testProp1 = 'test 1';
    testModel1.testProp2 = 1;

    const testModel2 = new TestModel();
    testModel2.testProp1 = 'test 2';
    testModel2.testProp2 = 2;

    testEntityListService.addNewEntity(testModel1);
    testEntityListService.addNewEntity(testModel2);

    // Act
    testEntityListService.removeEntity(-1);

    // Assert
    const expectedResult: TestModel[] = [];

    const expectedTestModel2 = new TestModel();
    expectedTestModel2.id = -2;
    expectedTestModel2.testProp1 = 'test 2';
    expectedTestModel2.testProp2 = 2;

    expectedResult.push(expectedTestModel2);

    console.log(`Actual: ${testEntityListService.entityList.value.length}`);
    console.log(`Expected: ${expectedResult.length}`);

    expect(testEntityListService.entityList.value).toEqual(expectedResult);
  });

  it('should get existing entity', () => {
    // Arrange
    const testModel1 = new TestModel();
    testModel1.testProp1 = 'test 1';
    testModel1.testProp2 = 1;

    const testModel2 = new TestModel();
    testModel2.testProp1 = 'test 2';
    testModel2.testProp2 = 2;

    testEntityListService.addNewEntity(testModel1);
    testEntityListService.addNewEntity(testModel2);

    // Act
    const actualResult = testEntityListService.getEntity(-2);

    // Assert
    const expectedResult = new TestModel();
    expectedResult.id = -2;
    expectedResult.testProp1 = 'test 2';
    expectedResult.testProp2 = 2;

    expect(actualResult).toEqual(expectedResult);
  });

  it('should reset entity list', () => {
    // Arrange
    const testModel1 = new TestModel();
    testModel1.testProp1 = 'test 1';
    testModel1.testProp2 = 1;

    const testModel2 = new TestModel();
    testModel2.testProp1 = 'test 2';
    testModel2.testProp2 = 2;

    testEntityListService.addNewEntity(testModel1);
    testEntityListService.addNewEntity(testModel2);

    // Act
    testEntityListService.resetEntityList();

    // Assert
    const expectedResult = [];
    expect(testEntityListService.entityList.value).toEqual(expectedResult);
  });
});
