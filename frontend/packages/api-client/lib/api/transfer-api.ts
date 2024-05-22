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
  RequestArgs,
  BaseAPI,
  RequiredError,
  operationServerMap,
} from '../base';
// @ts-ignore
import { CreateTransferRequest } from '../model';
// @ts-ignore
import { HttpValidationProblemDetails } from '../model';
// @ts-ignore
import { ProblemDetails } from '../model';
// @ts-ignore
import { TransferDTOType } from '../model';
// @ts-ignore
import { TransfersQueryResponse } from '../model';
// @ts-ignore
import { UpdateTransferRequest } from '../model';
/**
 * TransferApi - axios parameter creator
 * @export
 */
export const TransferApiAxiosParamCreator = function (
  configuration?: Configuration
) {
  return {
    /**
     *
     * @param {string} id
     * @param {CreateTransferRequest} createTransferRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    addTransfer: async (
      id: string,
      createTransferRequest: CreateTransferRequest,
      options: RawAxiosRequestConfig = {}
    ): Promise<RequestArgs> => {
      // verify required parameter 'id' is not null or undefined
      assertParamExists('addTransfer', 'id', id);
      // verify required parameter 'createTransferRequest' is not null or undefined
      assertParamExists(
        'addTransfer',
        'createTransferRequest',
        createTransferRequest
      );
      const localVarPath = `/budget/{id}/transfer`.replace(
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
        createTransferRequest,
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
     * @param {string} transferId
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    deleteTransfer: async (
      id: string,
      transferId: string,
      options: RawAxiosRequestConfig = {}
    ): Promise<RequestArgs> => {
      // verify required parameter 'id' is not null or undefined
      assertParamExists('deleteTransfer', 'id', id);
      // verify required parameter 'transferId' is not null or undefined
      assertParamExists('deleteTransfer', 'transferId', transferId);
      const localVarPath = `/budget/{id}/transfer/{transferId}`
        .replace(`{${'id'}}`, encodeURIComponent(String(id)))
        .replace(`{${'transferId'}}`, encodeURIComponent(String(transferId)));
      // use dummy base URL string because the URL constructor only accepts absolute URLs.
      const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }

      const localVarRequestOptions = {
        method: 'DELETE',
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
     * @param {string} transferId
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getTransfer: async (
      id: string,
      transferId: string,
      options: RawAxiosRequestConfig = {}
    ): Promise<RequestArgs> => {
      // verify required parameter 'id' is not null or undefined
      assertParamExists('getTransfer', 'id', id);
      // verify required parameter 'transferId' is not null or undefined
      assertParamExists('getTransfer', 'transferId', transferId);
      const localVarPath = `/budget/{id}/transfer/{transferId}`
        .replace(`{${'id'}}`, encodeURIComponent(String(id)))
        .replace(`{${'transferId'}}`, encodeURIComponent(String(transferId)));
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
     * @param {TransferDTOType} [type]
     * @param {Date} [dateFrom]
     * @param {Date} [dateTo]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getTransfers: async (
      id: string,
      type?: TransferDTOType,
      dateFrom?: Date,
      dateTo?: Date,
      options: RawAxiosRequestConfig = {}
    ): Promise<RequestArgs> => {
      // verify required parameter 'id' is not null or undefined
      assertParamExists('getTransfers', 'id', id);
      const localVarPath = `/budget/{id}/transfer`.replace(
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

      if (type !== undefined) {
        localVarQueryParameter['type'] = type;
      }

      if (dateFrom !== undefined) {
        localVarQueryParameter['dateFrom'] =
          (dateFrom as any) instanceof Date
            ? (dateFrom as any).toISOString()
            : dateFrom;
      }

      if (dateTo !== undefined) {
        localVarQueryParameter['dateTo'] =
          (dateTo as any) instanceof Date
            ? (dateTo as any).toISOString()
            : dateTo;
      }

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
     * @param {string} transferId
     * @param {UpdateTransferRequest} updateTransferRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    updateTransfer: async (
      id: string,
      transferId: string,
      updateTransferRequest: UpdateTransferRequest,
      options: RawAxiosRequestConfig = {}
    ): Promise<RequestArgs> => {
      // verify required parameter 'id' is not null or undefined
      assertParamExists('updateTransfer', 'id', id);
      // verify required parameter 'transferId' is not null or undefined
      assertParamExists('updateTransfer', 'transferId', transferId);
      // verify required parameter 'updateTransferRequest' is not null or undefined
      assertParamExists(
        'updateTransfer',
        'updateTransferRequest',
        updateTransferRequest
      );
      const localVarPath = `/budget/{id}/transfer/{transferId}`
        .replace(`{${'id'}}`, encodeURIComponent(String(id)))
        .replace(`{${'transferId'}}`, encodeURIComponent(String(transferId)));
      // use dummy base URL string because the URL constructor only accepts absolute URLs.
      const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
      let baseOptions;
      if (configuration) {
        baseOptions = configuration.baseOptions;
      }

      const localVarRequestOptions = {
        method: 'PUT',
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
        updateTransferRequest,
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
 * TransferApi - functional programming interface
 * @export
 */
export const TransferApiFp = function (configuration?: Configuration) {
  const localVarAxiosParamCreator = TransferApiAxiosParamCreator(configuration);
  return {
    /**
     *
     * @param {string} id
     * @param {CreateTransferRequest} createTransferRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async addTransfer(
      id: string,
      createTransferRequest: CreateTransferRequest,
      options?: RawAxiosRequestConfig
    ): Promise<
      (axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>
    > {
      const localVarAxiosArgs = await localVarAxiosParamCreator.addTransfer(
        id,
        createTransferRequest,
        options
      );
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['TransferApi.addTransfer']?.[
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
     * @param {string} transferId
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async deleteTransfer(
      id: string,
      transferId: string,
      options?: RawAxiosRequestConfig
    ): Promise<
      (axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>
    > {
      const localVarAxiosArgs = await localVarAxiosParamCreator.deleteTransfer(
        id,
        transferId,
        options
      );
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['TransferApi.deleteTransfer']?.[
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
     * @param {string} transferId
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async getTransfer(
      id: string,
      transferId: string,
      options?: RawAxiosRequestConfig
    ): Promise<
      (axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>
    > {
      const localVarAxiosArgs = await localVarAxiosParamCreator.getTransfer(
        id,
        transferId,
        options
      );
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['TransferApi.getTransfer']?.[
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
     * @param {TransferDTOType} [type]
     * @param {Date} [dateFrom]
     * @param {Date} [dateTo]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async getTransfers(
      id: string,
      type?: TransferDTOType,
      dateFrom?: Date,
      dateTo?: Date,
      options?: RawAxiosRequestConfig
    ): Promise<
      (
        axios?: AxiosInstance,
        basePath?: string
      ) => AxiosPromise<TransfersQueryResponse>
    > {
      const localVarAxiosArgs = await localVarAxiosParamCreator.getTransfers(
        id,
        type,
        dateFrom,
        dateTo,
        options
      );
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['TransferApi.getTransfers']?.[
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
     * @param {string} transferId
     * @param {UpdateTransferRequest} updateTransferRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async updateTransfer(
      id: string,
      transferId: string,
      updateTransferRequest: UpdateTransferRequest,
      options?: RawAxiosRequestConfig
    ): Promise<
      (axios?: AxiosInstance, basePath?: string) => AxiosPromise<void>
    > {
      const localVarAxiosArgs = await localVarAxiosParamCreator.updateTransfer(
        id,
        transferId,
        updateTransferRequest,
        options
      );
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['TransferApi.updateTransfer']?.[
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
 * TransferApi - factory interface
 * @export
 */
export const TransferApiFactory = function (
  configuration?: Configuration,
  basePath?: string,
  axios?: AxiosInstance
) {
  const localVarFp = TransferApiFp(configuration);
  return {
    /**
     *
     * @param {string} id
     * @param {CreateTransferRequest} createTransferRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    addTransfer(
      id: string,
      createTransferRequest: CreateTransferRequest,
      options?: any
    ): AxiosPromise<void> {
      return localVarFp
        .addTransfer(id, createTransferRequest, options)
        .then((request) => request(axios, basePath));
    },
    /**
     *
     * @param {string} id
     * @param {string} transferId
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    deleteTransfer(
      id: string,
      transferId: string,
      options?: any
    ): AxiosPromise<void> {
      return localVarFp
        .deleteTransfer(id, transferId, options)
        .then((request) => request(axios, basePath));
    },
    /**
     *
     * @param {string} id
     * @param {string} transferId
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getTransfer(
      id: string,
      transferId: string,
      options?: any
    ): AxiosPromise<void> {
      return localVarFp
        .getTransfer(id, transferId, options)
        .then((request) => request(axios, basePath));
    },
    /**
     *
     * @param {string} id
     * @param {TransferDTOType} [type]
     * @param {Date} [dateFrom]
     * @param {Date} [dateTo]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getTransfers(
      id: string,
      type?: TransferDTOType,
      dateFrom?: Date,
      dateTo?: Date,
      options?: any
    ): AxiosPromise<TransfersQueryResponse> {
      return localVarFp
        .getTransfers(id, type, dateFrom, dateTo, options)
        .then((request) => request(axios, basePath));
    },
    /**
     *
     * @param {string} id
     * @param {string} transferId
     * @param {UpdateTransferRequest} updateTransferRequest
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    updateTransfer(
      id: string,
      transferId: string,
      updateTransferRequest: UpdateTransferRequest,
      options?: any
    ): AxiosPromise<void> {
      return localVarFp
        .updateTransfer(id, transferId, updateTransferRequest, options)
        .then((request) => request(axios, basePath));
    },
  };
};

/**
 * TransferApi - interface
 * @export
 * @interface TransferApi
 */
export interface TransferApiInterface {
  /**
   *
   * @param {string} id
   * @param {CreateTransferRequest} createTransferRequest
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof TransferApiInterface
   */
  addTransfer(
    id: string,
    createTransferRequest: CreateTransferRequest,
    options?: RawAxiosRequestConfig
  ): AxiosPromise<void>;

  /**
   *
   * @param {string} id
   * @param {string} transferId
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof TransferApiInterface
   */
  deleteTransfer(
    id: string,
    transferId: string,
    options?: RawAxiosRequestConfig
  ): AxiosPromise<void>;

  /**
   *
   * @param {string} id
   * @param {string} transferId
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof TransferApiInterface
   */
  getTransfer(
    id: string,
    transferId: string,
    options?: RawAxiosRequestConfig
  ): AxiosPromise<void>;

  /**
   *
   * @param {string} id
   * @param {TransferDTOType} [type]
   * @param {Date} [dateFrom]
   * @param {Date} [dateTo]
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof TransferApiInterface
   */
  getTransfers(
    id: string,
    type?: TransferDTOType,
    dateFrom?: Date,
    dateTo?: Date,
    options?: RawAxiosRequestConfig
  ): AxiosPromise<TransfersQueryResponse>;

  /**
   *
   * @param {string} id
   * @param {string} transferId
   * @param {UpdateTransferRequest} updateTransferRequest
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof TransferApiInterface
   */
  updateTransfer(
    id: string,
    transferId: string,
    updateTransferRequest: UpdateTransferRequest,
    options?: RawAxiosRequestConfig
  ): AxiosPromise<void>;
}

/**
 * TransferApi - object-oriented interface
 * @export
 * @class TransferApi
 * @extends {BaseAPI}
 */
export class TransferApi extends BaseAPI implements TransferApiInterface {
  /**
   *
   * @param {string} id
   * @param {CreateTransferRequest} createTransferRequest
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof TransferApi
   */
  public addTransfer(
    id: string,
    createTransferRequest: CreateTransferRequest,
    options?: RawAxiosRequestConfig
  ) {
    return TransferApiFp(this.configuration)
      .addTransfer(id, createTransferRequest, options)
      .then((request) => request(this.axios, this.basePath));
  }

  /**
   *
   * @param {string} id
   * @param {string} transferId
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof TransferApi
   */
  public deleteTransfer(
    id: string,
    transferId: string,
    options?: RawAxiosRequestConfig
  ) {
    return TransferApiFp(this.configuration)
      .deleteTransfer(id, transferId, options)
      .then((request) => request(this.axios, this.basePath));
  }

  /**
   *
   * @param {string} id
   * @param {string} transferId
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof TransferApi
   */
  public getTransfer(
    id: string,
    transferId: string,
    options?: RawAxiosRequestConfig
  ) {
    return TransferApiFp(this.configuration)
      .getTransfer(id, transferId, options)
      .then((request) => request(this.axios, this.basePath));
  }

  /**
   *
   * @param {string} id
   * @param {TransferDTOType} [type]
   * @param {Date} [dateFrom]
   * @param {Date} [dateTo]
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof TransferApi
   */
  public getTransfers(
    id: string,
    type?: TransferDTOType,
    dateFrom?: Date,
    dateTo?: Date,
    options?: RawAxiosRequestConfig
  ) {
    return TransferApiFp(this.configuration)
      .getTransfers(id, type, dateFrom, dateTo, options)
      .then((request) => request(this.axios, this.basePath));
  }

  /**
   *
   * @param {string} id
   * @param {string} transferId
   * @param {UpdateTransferRequest} updateTransferRequest
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof TransferApi
   */
  public updateTransfer(
    id: string,
    transferId: string,
    updateTransferRequest: UpdateTransferRequest,
    options?: RawAxiosRequestConfig
  ) {
    return TransferApiFp(this.configuration)
      .updateTransfer(id, transferId, updateTransferRequest, options)
      .then((request) => request(this.axios, this.basePath));
  }
}
