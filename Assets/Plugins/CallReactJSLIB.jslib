mergeInto(LibraryManager.library, {
  GameInit: function (message) {
    try {
      window.dispatchReactUnityEvent("GameInit", UTF8ToString(message));
    } catch (e) {
      console.warn("Failed to dispatch event");
    }
  }
});