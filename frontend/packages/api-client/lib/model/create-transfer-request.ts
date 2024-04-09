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

// May contain unused imports in some cases
// @ts-ignore
import { TransferDTOType } from './transfer-dtotype';

/**
 *
 * @export
 * @interface CreateTransferRequest
 */
export interface CreateTransferRequest {
  /**
   *
   * @type {TransferDTOType}
   * @memberof CreateTransferRequest
   */
  type?: TransferDTOType;
  /**
   *
   * @type {string}
   * @memberof CreateTransferRequest
   */
  name?: string | null;
  /**
   *
   * @type {number}
   * @memberof CreateTransferRequest
   */
  value?: number;
  /**
   *
   * @type {string}
   * @memberof CreateTransferRequest
   */
  currency?: string | null;
  /**
   *
   * @type {string}
   * @memberof CreateTransferRequest
   */
  category?: string | null;
  /**
   *
   * @type {string}
   * @memberof CreateTransferRequest
   */
  date?: string | null;
}
