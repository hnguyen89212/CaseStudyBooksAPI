export function formatPrice(price) {
  let price_ = "" + price;
  const decimalPointIndex = price_.indexOf(".");
  return Number.parseFloat(price_.substr(0, decimalPointIndex + 2));
}
