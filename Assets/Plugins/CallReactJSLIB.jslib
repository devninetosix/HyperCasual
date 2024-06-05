mergeInto(LibraryManager.library, {
  GameInit: function (message) {
    try {
      window.dispatchReactUnityEvent("GameInit", message);
    } catch (e) {
      console.warn("Failed to dispatch event");
    }
  }
});