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
import { BudgetTotals } from '../model';
// @ts-ignore
import { CategoryValue } from '../model';
// @ts-ignore
import { ProblemDetails } from '../model';
// @ts-ignore
import { TransferDTOType } from '../model';
/**
 * BudgetStatisticsApi - axios parameter creator
 * @export
 */
export const BudgetStatisticsApiAxiosParamCreator = function (
  configuration?: Configuration
) {
  return {
    /**
     *
     * @param {string} id
     * @param {number} [year]
     * @param {number} [month]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getBudgetTotals: async (
      id: string,
      year?: number,
      month?: number,
      options: RawAxiosRequestConfig = {}
    ): Promise<RequestArgs> => {
      // verify required parameter 'id' is not null or undefined
      assertParamExists('getBudgetTotals', 'id', id);
      const localVarPath = `/budget/{id}/totals`.replace(
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

      if (year !== undefined) {
        localVarQueryParameter['year'] = year;
      }

      if (month !== undefined) {
        localVarQueryParameter['month'] = month;
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
     * @param {TransferDTOType} type
     * @param {number} [year]
     * @param {number} [month]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getBudgetTransfersTotalsGropedByCategory: async (
      id: string,
      type: TransferDTOType,
      year?: number,
      month?: number,
      options: RawAxiosRequestConfig = {}
    ): Promise<RequestArgs> => {
      // verify required parameter 'id' is not null or undefined
      assertParamExists('getBudgetTransfersTotalsGropedByCategory', 'id', id);
      // verify required parameter 'type' is not null or undefined
      assertParamExists(
        'getBudgetTransfersTotalsGropedByCategory',
        'type',
        type
      );
      const localVarPath = `/budget/{id}/totals/grouped-by-category`.replace(
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

      if (type !== undefined) {
        localVarQueryParameter['type'] = type;
      }

      if (year !== undefined) {
        localVarQueryParameter['year'] = year;
      }

      if (month !== undefined) {
        localVarQueryParameter['month'] = month;
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
  };
};

/**
 * BudgetStatisticsApi - functional programming interface
 * @export
 */
export const BudgetStatisticsApiFp = function (configuration?: Configuration) {
  const localVarAxiosParamCreator =
    BudgetStatisticsApiAxiosParamCreator(configuration);
  return {
    /**
     *
     * @param {string} id
     * @param {number} [year]
     * @param {number} [month]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async getBudgetTotals(
      id: string,
      year?: number,
      month?: number,
      options?: RawAxiosRequestConfig
    ): Promise<
      (axios?: AxiosInstance, basePath?: string) => AxiosPromise<BudgetTotals>
    > {
      const localVarAxiosArgs = await localVarAxiosParamCreator.getBudgetTotals(
        id,
        year,
        month,
        options
      );
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap['BudgetStatisticsApi.getBudgetTotals']?.[
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
     * @param {TransferDTOType} type
     * @param {number} [year]
     * @param {number} [month]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    async getBudgetTransfersTotalsGropedByCategory(
      id: string,
      type: TransferDTOType,
      year?: number,
      month?: number,
      options?: RawAxiosRequestConfig
    ): Promise<
      (
        axios?: AxiosInstance,
        basePath?: string
      ) => AxiosPromise<Array<CategoryValue>>
    > {
      const localVarAxiosArgs =
        await localVarAxiosParamCreator.getBudgetTransfersTotalsGropedByCategory(
          id,
          type,
          year,
          month,
          options
        );
      const localVarOperationServerIndex = configuration?.serverIndex ?? 0;
      const localVarOperationServerBasePath =
        operationServerMap[
          'BudgetStatisticsApi.getBudgetTransfersTotalsGropedByCategory'
        ]?.[localVarOperationServerIndex]?.url;
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
 * BudgetStatisticsApi - factory interface
 * @export
 */
export const BudgetStatisticsApiFactory = function (
  configuration?: Configuration,
  basePath?: string,
  axios?: AxiosInstance
) {
  const localVarFp = BudgetStatisticsApiFp(configuration);
  return {
    /**
     *
     * @param {string} id
     * @param {number} [year]
     * @param {number} [month]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getBudgetTotals(
      id: string,
      year?: number,
      month?: number,
      options?: any
    ): AxiosPromise<BudgetTotals> {
      return localVarFp
        .getBudgetTotals(id, year, month, options)
        .then((request) => request(axios, basePath));
    },
    /**
     *
     * @param {string} id
     * @param {TransferDTOType} type
     * @param {number} [year]
     * @param {number} [month]
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     */
    getBudgetTransfersTotalsGropedByCategory(
      id: string,
      type: TransferDTOType,
      year?: number,
      month?: number,
      options?: any
    ): AxiosPromise<Array<CategoryValue>> {
      return localVarFp
        .getBudgetTransfersTotalsGropedByCategory(
          id,
          type,
          year,
          month,
          options
        )
        .then((request) => request(axios, basePath));
    },
  };
};

/**
 * BudgetStatisticsApi - interface
 * @export
 * @interface BudgetStatisticsApi
 */
export interface BudgetStatisticsApiInterface {
  /**
   *
   * @param {string} id
   * @param {number} [year]
   * @param {number} [month]
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof BudgetStatisticsApiInterface
   */
  getBudgetTotals(
    id: string,
    year?: number,
    month?: number,
    options?: RawAxiosRequestConfig
  ): AxiosPromise<BudgetTotals>;

  /**
   *
   * @param {string} id
   * @param {TransferDTOType} type
   * @param {number} [year]
   * @param {number} [month]
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof BudgetStatisticsApiInterface
   */
  getBudgetTransfersTotalsGropedByCategory(
    id: string,
    type: TransferDTOType,
    year?: number,
    month?: number,
    options?: RawAxiosRequestConfig
  ): AxiosPromise<Array<CategoryValue>>;
}

/**
 * BudgetStatisticsApi - object-oriented interface
 * @export
 * @class BudgetStatisticsApi
 * @extends {BaseAPI}
 */
export class BudgetStatisticsApi
  extends BaseAPI
  implements BudgetStatisticsApiInterface
{
  /**
   *
   * @param {string} id
   * @param {number} [year]
   * @param {number} [month]
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof BudgetStatisticsApi
   */
  public getBudgetTotals(
    id: string,
    year?: number,
    month?: number,
    options?: RawAxiosRequestConfig
  ) {
    return BudgetStatisticsApiFp(this.configuration)
      .getBudgetTotals(id, year, month, options)
      .then((request) => request(this.axios, this.basePath));
  }

  /**
   *
   * @param {string} id
   * @param {TransferDTOType} type
   * @param {number} [year]
   * @param {number} [month]
   * @param {*} [options] Override http request option.
   * @throws {RequiredError}
   * @memberof BudgetStatisticsApi
   */
  public getBudgetTransfersTotalsGropedByCategory(
    id: string,
    type: TransferDTOType,
    year?: number,
    month?: number,
    options?: RawAxiosRequestConfig
  ) {
    return BudgetStatisticsApiFp(this.configuration)
      .getBudgetTransfersTotalsGropedByCategory(id, type, year, month, options)
      .then((request) => request(this.axios, this.basePath));
  }
}
