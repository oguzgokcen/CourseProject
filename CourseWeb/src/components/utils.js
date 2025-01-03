function clearNumber(value = '') {
  return value.replace(/\D+/g, '')
}

export function formatCreditCardNumber(value) {
  if (!value) {
    return value
  }

  const clearValue = clearNumber(value)
  let nextValue

  nextValue = `${clearValue.slice(0, 4)} ${clearValue.slice(
    4,
    8
  )} ${clearValue.slice(8, 12)} ${clearValue.slice(12, 19)}`

  return nextValue.trim()
}

export function formatCVC(value) {
  const clearValue = clearNumber(value)
  let maxLength = 3

  return clearValue.slice(0, maxLength)
}

export function formatExpirationDate(value) {
  const clearValue = clearNumber(value)

  if (clearValue.length >= 3) {
    return `${clearValue.slice(0, 2)}/${clearValue.slice(2, 4)}`
  }

  return clearValue
}

export function validateCardNumber(cardNumber) {
  
  var number = clearNumber(cardNumber)
  let sum = 0;
  let shouldDouble = false;

  for (let i = number.length - 1; i >= 0; i--) {
    let digit = parseInt(number.charAt(i), 10);

    if (shouldDouble) {
      digit *= 2;
      if (digit > 9) {
        digit -= 9;
      }
    }

    sum += digit;
    shouldDouble = !shouldDouble;
  }

  return sum % 10 === 0;
}
