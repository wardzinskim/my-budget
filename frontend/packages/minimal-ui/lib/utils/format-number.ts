/* tslint:disable */
/* eslint-disable */
import numeral from 'numeral';

// ----------------------------------------------------------------------

export function fNumber(number: any) {
  return numeral(number).format();
}

export function fCurrency(number: any) {
  const format = number ? numeral(number).format('0,0.00') : '';

  return result(format, '.00');
}

export function fPercent(number: any) {
  const format = number ? numeral(Number(number) / 100).format('0.0%') : '';

  return result(format, '.0');
}

function result(format: string, key = '.00') {
  const isInteger = format.includes(key);

  return isInteger ? format.replace(key, '') : format;
}
