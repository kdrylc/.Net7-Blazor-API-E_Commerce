redirectToCheckout = function (sessionId) {
    var stripe = Stripe("pk_test_51NdpprEDHM07fE30Z8BMDQ1MhUJD9ny7M15G5BnFet5Jkv58Sp40XUwNv2fZh0R8UeorLiy2HIoivDXZFaA1nR9900vLPh8m3m");
    stripe.redirectToCheckout({ sessionId: sessionId });
}