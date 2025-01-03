/* tslint:disable */
/* eslint-disable */
/**
 * MyBudget API
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v1
 *
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import type { Configuration } from '../configuration';
import type { AxiosPromise, AxiosInstance, RawAxiosRequestConfig } from 'axios';
import globalAxios from 'axios';
// Some imports not used depending on template conditions
// @ts-ignore
import {
  DUMMY_BASE_URL,
  assertParamExists,
  setApiKeyToObject,
  setBasicAuthToObject,
  setBearerAuthToObject,
  setOAuthToObject,
  setSearchParams,
  serializeDataIfNeeded,
  toPathString,
  createRequestFunction,
} from '../common';
// @ts-ignore
import {
  BASE_PATH,
  COLLECTION_FORMATS,
  type RequestArgs,
  BaseAPI,
  RequiredError,
  operationServerMap,
} from '../base';
// @ts-ignore
import type { BudgetDTO } from '../model';
// @ts-ignore
import type { CreateBudgetRequest } from '../model';
// @ts-ignore
import type { HttpValidationProblemDetails } from '../model';
// @ts-ignore
import type { ProblemDetails } from '../model';
// @ts-ignore
import type { ShareBudgetRequest } from '../model';
/**
 * BudgetApi - axios parameter creator
 * @export
 */
export const BudgetApiAxiosParamCreator = function (
  configuration?: Configuration
) {
  return {
    /**
     *
     * @param {CreateBudgetRequest} createBudgetRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    createBudget: async (
      createBudgetRequest: CreateBudgetRequest,
      options: RawAxiosRequestConfig = {}
    ): Promise<RequestArgs> => {
      // verify required parameter 'createBudgetRequest' is not null or undefined
      assertParamExists(
        'createBudget',
        'createBudgetRequest',
        createBudgetRequest
      );
      const localVarPath = `/budget`;
      // use dummy base URL string because the URL constructor only accepts absolute URLs.
      const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }

      const localVarRequestOptions = {
        method: 'POST',
        ...baseOptions,
        ...options,
      };
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication Bearer required
      // http bearer authentication required
      await setBearerAuthToObject(localVarHeaderParameter, configuration);

      localVarHeaderParameter['Content-Type'] = 'application/json';

      setSearchParams(localVarUrlObj, localVarQueryParameter);
      let headersFromBaseOptions =
        baseOptions && baseOptions.headers ? baseOptions.headers : {};
      localVarRequestOptions.headers = {
        ...localVarHeaderParameter,
        ...headersFromBaseOptions,
        ...options.headers,
      };
      localVarRequestOptions.data = serializeDataIfNeeded(
        createBudgetRequest,
        localVarRequestOptions,
        configuration
      );

      return {
        url: toPathString(localVarUrlObj),
        options: localVarRequestOptions,
      };
    },
    /**
     *
     * @param {string} id
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getBudget: async (
      id: string,
      options: RawAxiosRequestConfig = {}
    ): Promise<RequestArgs> => {
      // verify required parameter 'id' is not null or undefined
      assertParamExists('getBudget', 'id', id);
      const localVarPath = `/budget/{id}`.replace(
        `{${'id'}}`,
        encodeURIComponent(String(id))
      );
      // use dummy base URL string because the URL constructor only accepts absolute URLs.
      const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }

      const localVarRequestOptions = {
        method: 'GET',
        ...baseOptions,
        ...options,
      };
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication Bearer required
      // http bearer authentication required
      await setBearerAuthToObject(localVarHeaderParameter, configuration);

      setSearchParams(localVarUrlObj, localVarQueryParameter);
      let headersFromBaseOptions =
        baseOptions && baseOptions.headers ? baseOptions.headers : {};
      localVarRequestOptions.headers = {
        ...localVarHeaderParameter,
        ...headersFromBaseOptions,
        ...options.headers,
      };

      return {
        url: toPathString(localVarUrlObj),
        options: localVarRequestOptions,
      };
    },
    /**
     *
     * @param {string} id
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getBudget_1: async (
      id: string,
      options: RawAxiosRequestConfig = {}
    ): Promise<RequestArgs> => {
      // verify required parameter 'id' is not null or undefined
      assertParamExists('getBudget_1', 'id', id);
      const localVarPath = `/budget/{id}`.replace(
        `{${'id'}}`,
        encodeURIComponent(String(id))
      );
      // use dummy base URL string because the URL constructor only accepts absolute URLs.
      const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }

      const localVarRequestOptions = {
        method: 'GET',
        ...baseOptions,
        ...options,
      };
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication Bearer required
      // http bearer authentication required
      await setBearerAuthToObject(localVarHeaderParameter, configuration);

      setSearchParams(localVarUrlObj, localVarQueryParameter);
      let headersFromBaseOptions =
        baseOptions && baseOptions.headers ? baseOptions.headers : {};
      localVarRequestOptions.headers = {
        ...localVarHeaderParameter,
        ...headersFromBaseOptions,
        ...options.headers,
      };

      return {
        url: toPathString(localVarUrlObj),
        options: localVarRequestOptions,
      };
    },
    /**
     *
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getBudgets: async (
      options: RawAxiosRequestConfig = {}
    ): Promise<RequestArgs> => {
      const localVarPath = `/budget`;
      // use dummy base URL string because the URL constructor only accepts absolute URLs.
      const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }

      const localVarRequestOptions = {
        method: 'GET',
        ...baseOptions,
        ...options,
      };
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication Bearer required
      // http bearer authentication required
      await setBearerAuthToObject(localVarHeaderParameter, configuration);

      setSearchParams(localVarUrlObj, localVarQueryParameter);
      let headersFromBaseOptions =
        baseOptions && baseOptions.headers ? baseOptions.headers : {};
      localVarRequestOptions.headers = {
        ...localVarHeaderParameter,
        ...headersFromBaseOptions,
        ...options.headers,
      };

      return {
        url: toPathString(localVarUrlObj),
        options: localVarRequestOptions,
      };
    },
    /**
     *
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getBudgets_2: async (
      options: RawAxiosRequestConfig = {}
    ): Promise<RequestArgs> => {
      const localVarPath = `/budget`;
      // use dummy base URL string because the URL constructor only accepts absolute URLs.
      const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }

      const localVarRequestOptions = {
        method: 'GET',
        ...baseOptions,
        ...options,
      };
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication Bearer required
      // http bearer authentication required
      await setBearerAuthToObject(localVarHeaderParameter, configuration);

      setSearchParams(localVarUrlObj, localVarQueryParameter);
      let headersFromBaseOptions =
        baseOptions && baseOptions.headers ? baseOptions.headers : {};
      localVarRequestOptions.headers = {
        ...localVarHeaderParameter,
        ...headersFromBaseOptions,
        ...options.headers,
      };

      return {
        url: toPathString(localVarUrlObj),
        options: localVarRequestOptions,
      };
    },
    /**
     *
     * @param {string} id
     * @param {ShareBudgetRequest} shareBudgetRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    shareBudget: async (
      id: string,
      shareBudgetRequest: ShareBudgetRequest,
      options: RawAxiosRequestConfig = {}
    ): Promise<RequestArgs> => {
      // verify required parameter 'id' is not null or undefined
      assertParamExists('shareBudget', 'id', id);
      // verify required parameter 'shareBudgetRequest' is not null or undefined
      assertParamExists(
        'shareBudget',
        'shareBudgetRequest',
        shareBudgetRequest
      );
      const localVarPath = `/budget/{id}/share`.replace(
        `{${'id'}}`,
        encodeURIComponent(String(id))
      );
      // use dummy base URL string because the URL constructor only accepts absolute URLs.
      const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }

      const localVarRequestOptions = {
        method: 'POST',
        ...baseOptions,
        ...options,
      };
      const localVarHeaderParameter = {} as any;
      const localVarQueryParameter = {} as any;

      // authentication Bearer required
      // http bearer authentication required
      await setBearerAuthToObject(localVarHeaderParameter, configuration);

      localVarHeaderParameter['Content-Type'] = 'application/json';

      setSearchParams(localVarUrlObj, localVarQueryParameter);
      let headersFromBaseOptions =
        baseOptions && baseOptions.headers ? baseOptions.headers : {};
      localVarRequestOptions.headers = {
        ...localVarHeaderParameter,
        ...headersFromBaseOptions,
        ...options.headers,
      };
      localVarRequestOptions.data = serializeDataIfNeeded(
        shareBudgetRequest,
        localVarRequestOptions,
        configuration
      );

      return {
        url: toPathString(localVarUrlObj),
        options: localVarRequestOptions,
      };
    },
  };
};

/**
 * BudgetApi - functional programming interface
 * @export
 */
export const BudgetApiFp = function (configuration?: Configuration) {
  const localVarAxiosParamCreator = BudgetApiAxiosParamCreator(configuration);
  return {
    /**
     *
     * @param {CreateBudgetRequest} createBudgetRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async createBudget(
      createBudgetRequest: CreateBudgetRequest,
      options?: RawAxiosRequestConfig
    ): Promise<
      (axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>
    > {
      const localVarAxiosArgs = await localVarAxiosParamCreator.createBudget(
        createBudgetRequest,
        options
      );
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['BudgetApi.createBudget']?.[
          localVarOperationServerIndex
        ]?.url;
      return (axios, basePath) =>
        createRequestFunction(
          localVarAxiosArgs,
          globalAxios,
          BASE_PATH,
          configuration
        )(axios, localVarOperationServerBasePath || basePath);
    },
    /**
     *
     * @param {string} id
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async getBudget(
      id: string,
      options?: RawAxiosRequestConfig
    ): Promise<
      (axios?: AxiosInstance, basePath?: string) => AxiosPromise<BudgetDTO>
    > {
      const localVarAxiosArgs = await localVarAxiosParamCreator.getBudget(
        id,
        options
      );
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['BudgetApi.getBudget']?.[
          localVarOperationServerIndex
        ]?.url;
      return (axios, basePath) =>
        createRequestFunction(
          localVarAxiosArgs,
          globalAxios,
          BASE_PATH,
          configuration
        )(axios, localVarOperationServerBasePath || basePath);
    },
    /**
     *
     * @param {string} id
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async getBudget_1(
      id: string,
      options?: RawAxiosRequestConfig
    ): Promise<
      (axios?: AxiosInstance, basePath?: string) => AxiosPromise<BudgetDTO>
    > {
      const localVarAxiosArgs = await localVarAxiosParamCreator.getBudget_1(
        id,
        options
      );
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['BudgetApi.getBudget_1']?.[
          localVarOperationServerIndex
        ]?.url;
      return (axios, basePath) =>
        createRequestFunction(
          localVarAxiosArgs,
          globalAxios,
          BASE_PATH,
          configuration
        )(axios, localVarOperationServerBasePath || basePath);
    },
    /**
     *
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async getBudgets(
      options?: RawAxiosRequestConfig
    ): Promise<
      (
        axios?: AxiosInstance,
        basePath?: string
      ) => AxiosPromise<Array<BudgetDTO>>
    > {
      const localVarAxiosArgs =
        await localVarAxiosParamCreator.getBudgets(options);
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['BudgetApi.getBudgets']?.[
          localVarOperationServerIndex
        ]?.url;
      return (axios, basePath) =>
        createRequestFunction(
          localVarAxiosArgs,
          globalAxios,
          BASE_PATH,
          configuration
        )(axios, localVarOperationServerBasePath || basePath);
    },
    /**
     *
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async getBudgets_2(
      options?: RawAxiosRequestConfig
    ): Promise<
      (
        axios?: AxiosInstance,
        basePath?: string
      ) => AxiosPromise<Array<BudgetDTO>>
    > {
      const localVarAxiosArgs =
        await localVarAxiosParamCreator.getBudgets_2(options);
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['BudgetApi.getBudgets_2']?.[
          localVarOperationServerIndex
        ]?.url;
      return (axios, basePath) =>
        createRequestFunction(
          localVarAxiosArgs,
          globalAxios,
          BASE_PATH,
          configuration
        )(axios, localVarOperationServerBasePath || basePath);
    },
    /**
     *
     * @param {string} id
     * @param {ShareBudgetRequest} shareBudgetRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async shareBudget(
      id: string,
      shareBudgetRequest: ShareBudgetRequest,
      options?: RawAxiosRequestConfig
    ): Promise<
      (axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>
    > {
      const localVarAxiosArgs = await localVarAxiosParamCreator.shareBudget(
        id,
        shareBudgetRequest,
        options
      );
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['BudgetApi.shareBudget']?.[
          localVarOperationServerIndex
        ]?.url;
      return (axios, basePath) =>
        createRequestFunction(
          localVarAxiosArgs,
          globalAxios,
          BASE_PATH,
          configuration
        )(axios, localVarOperationServerBasePath || basePath);
    },
  };
};

/**
 * BudgetApi - factory interface
 * @export
 */
export const BudgetApiFactory = function (
  configuration?: Configuration,
  basePath?: string,
  axios?: AxiosInstance
) {
  const localVarFp = BudgetApiFp(configuration);
  return {
    /**
     *
     * @param {CreateBudgetRequest} createBudgetRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    createBudget(
      createBudgetRequest: CreateBudgetRequest,
      options?: RawAxiosRequestConfig
    ): AxiosPromise<void> {
      return localVarFp
        .createBudget(createBudgetRequest, options)
        .then((request) => request(axios, basePath));
    },
    /**
     *
     * @param {string} id
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getBudget(
      id: string,
      options?: RawAxiosRequestConfig
    ): AxiosPromise<BudgetDTO> {
      return localVarFp
        .getBudget(id, options)
        .then((request) => request(axios, basePath));
    },
    /**
     *
     * @param {string} id
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getBudget_1(
      id: string,
      options?: RawAxiosRequestConfig
    ): AxiosPromise<BudgetDTO> {
      return localVarFp
        .getBudget_1(id, options)
        .then((request) => request(axios, basePath));
    },
    /**
     *
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getBudgets(
      options?: RawAxiosRequestConfig
    ): AxiosPromise<Array<BudgetDTO>> {
      return localVarFp
        .getBudgets(options)
        .then((request) => request(axios, basePath));
    },
    /**
     *
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getBudgets_2(
      options?: RawAxiosRequestConfig
    ): AxiosPromise<Array<BudgetDTO>> {
      return localVarFp
        .getBudgets_2(options)
        .then((request) => request(axios, basePath));
    },
    /**
     *
     * @param {string} id
     * @param {ShareBudgetRequest} shareBudgetRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    shareBudget(
      id: string,
      shareBudgetRequest: ShareBudgetRequest,
      options?: RawAxiosRequestConfig
    ): AxiosPromise<void> {
      return localVarFp
        .shareBudget(id, shareBudgetRequest, options)
        .then((request) => request(axios, basePath));
    },
  };
};

/**
 * BudgetApi - interface
 * @export
 * @interface BudgetApi
 */
export interface BudgetApiInterface {
  /**
   *
   * @param {CreateBudgetRequest} createBudgetRequest
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof BudgetApiInterface
   */
  createBudget(
    createBudgetRequest: CreateBudgetRequest,
    options?: RawAxiosRequestConfig
  ): AxiosPromise<void>;

  /**
   *
   * @param {string} id
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof BudgetApiInterface
   */
  getBudget(
    id: string,
    options?: RawAxiosRequestConfig
  ): AxiosPromise<BudgetDTO>;

  /**
   *
   * @param {string} id
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof BudgetApiInterface
   */
  getBudget_1(
    id: string,
    options?: RawAxiosRequestConfig
  ): AxiosPromise<BudgetDTO>;

  /**
   *
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof BudgetApiInterface
   */
  getBudgets(options?: RawAxiosRequestConfig): AxiosPromise<Array<BudgetDTO>>;

  /**
   *
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof BudgetApiInterface
   */
  getBudgets_2(options?: RawAxiosRequestConfig): AxiosPromise<Array<BudgetDTO>>;

  /**
   *
   * @param {string} id
   * @param {ShareBudgetRequest} shareBudgetRequest
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof BudgetApiInterface
   */
  shareBudget(
    id: string,
    shareBudgetRequest: ShareBudgetRequest,
    options?: RawAxiosRequestConfig
  ): AxiosPromise<void>;
}

/**
 * BudgetApi - object-oriented interface
 * @export
 * @class BudgetApi
 * @extends {BaseAPI}
 */
export class BudgetApi extends BaseAPI implements BudgetApiInterface {
  /**
   *
   * @param {CreateBudgetRequest} createBudgetRequest
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof BudgetApi
   */
  public createBudget(
    createBudgetRequest: CreateBudgetRequest,
    options?: RawAxiosRequestConfig
  ) {
    return BudgetApiFp(this.configuration)
      .createBudget(createBudgetRequest, options)
      .then((request) => request(this.axios, this.basePath));
  }

  /**
   *
   * @param {string} id
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof BudgetApi
   */
  public getBudget(id: string, options?: RawAxiosRequestConfig) {
    return BudgetApiFp(this.configuration)
      .getBudget(id, options)
      .then((request) => request(this.axios, this.basePath));
  }

  /**
   *
   * @param {string} id
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof BudgetApi
   */
  public getBudget_1(id: string, options?: RawAxiosRequestConfig) {
    return BudgetApiFp(this.configuration)
      .getBudget_1(id, options)
      .then((request) => request(this.axios, this.basePath));
  }

  /**
   *
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof BudgetApi
   */
  public getBudgets(options?: RawAxiosRequestConfig) {
    return BudgetApiFp(this.configuration)
      .getBudgets(options)
      .then((request) => request(this.axios, this.basePath));
  }

  /**
   *
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof BudgetApi
   */
  public getBudgets_2(options?: RawAxiosRequestConfig) {
    return BudgetApiFp(this.configuration)
      .getBudgets_2(options)
      .then((request) => request(this.axios, this.basePath));
  }

  /**
   *
   * @param {string} id
   * @param {ShareBudgetRequest} shareBudgetRequest
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof BudgetApi
   */
  public shareBudget(
    id: string,
    shareBudgetRequest: ShareBudgetRequest,
    options?: RawAxiosRequestConfig
  ) {
    return BudgetApiFp(this.configuration)
      .shareBudget(id, shareBudgetRequest, options)
      .then((request) => request(this.axios, this.basePath));
  }
}
