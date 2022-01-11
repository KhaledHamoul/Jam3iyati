/*
Template Name: Admin Press Admin
Author: Themedesigner
Email: niravjoshi87@gmail.com
File: js
*/
(function (global, factory) {
  if (typeof define === "function" && define.amd) {
    define(['jquery'], factory);
  } else if (typeof exports !== "undefined") {
    factory(require('jquery'));
  } else {
    var mod = {
      exports: {}
    };
    factory(global.jquery);
    global.metisMenu = mod.exports;
  }
})(this, function (_jquery) {
  'use strict';

  var _jquery2 = _interopRequireDefault(_jquery);

  function _interopRequireDefault(obj) {
    return obj && obj.__esModule ? obj : {
      default: obj
    };
  }

  var _typeof = typeof Symbol === "function" && typeof Symbol.iterator === "symbol" ? function (obj) {
    return typeof obj;
  } : function (obj) {
    return obj && typeof Symbol === "function" && obj.constructor === Symbol && obj !== Symbol.prototype ? "symbol" : typeof obj;
  };

  function _classCallCheck(instance, Constructor) {
    if (!(instance instanceof Constructor)) {
      throw new TypeError("Cannot call a class as a function");
    }
  }

  var Util2 = function ($) {
    var transition = false;

    var TransitionEndEvent = {
      WebkitTransition: 'webkitTransitionEnd',
      MozTransition: 'transitionend',
      OTransition: 'oTransitionEnd otransitionend',
      transition: 'transitionend'
    };

    function getSpecialTransitionEndEvent() {
      return {
        bindType: transition.end,
        delegateType: transition.end,
        handle: function handle(event) {
          if ($(event.target).is(this)) {
            return event.handleObj.handler.apply(this, arguments);
          }
          return undefined;
        }
      };
    }

    function transitionEndTest() {
      if (window.QUnit) {
        return false;
      }

      var el = document.createElement('mm');

      for (var name in TransitionEndEvent) {
        if (el.style[name] !== undefined) {
          return {
            end: TransitionEndEvent[name]
          };
        }
      }

      return false;
    }

    function transitionEndEmulator(duration) {
      var _this2 = this;

      var called = false;

      $(this).one(Util2.TRANSITION_END, function () {
        called = true;
      });

      setTimeout(function () {
        if (!called) {
          Util2.triggerTransitionEnd(_this2);
        }
      }, duration);

      return this;
    }

    function setTransitionEndSupport() {
      transition = transitionEndTest();
      $.fn.emulateTransitionEnd = transitionEndEmulator;

      if (Util2.supportsTransitionEnd()) {
        $.event.special[Util2.TRANSITION_END] = getSpecialTransitionEndEvent();
      }
    }

    var Util2 = {
      TRANSITION_END: 'mmTransitionEnd',

      triggerTransitionEnd: function triggerTransitionEnd(element) {
        $(element).trigger(transition.end);
      },
      supportsTransitionEnd: function supportsTransitionEnd() {
        return Boolean(transition);
      }
    };

    setTransitionEndSupport();

    return Util2;
  }(jQuery);

  var MetisMenu = function ($) {

    var NAME = 'metisMenu';
    var DATA_KEY = 'metisMenu';
    var EVENT_KEY = '.' + DATA_KEY;
    var DATA_API_KEY = '.data-api';
    var JQUERY_NO_CONFLICT = $.fn[NAME];
    var TRANSITION_DURATION = 350;

    var Default2 = {
      toggle2: true,
      preventDefault2: true,
      activeClass2: 'active',
      collapseClass2: 'collapse',
      collapseInClass2: 'in',
      collapsingClass2: 'collapsing',
      triggerElement2: 'a',
      parentTrigger2: 'li',
      subMenu2: 'ul'
    };

    var Event = {
      SHOW: 'show' + EVENT_KEY,
      SHOWN: 'shown' + EVENT_KEY,
      HIDE: 'hide' + EVENT_KEY,
      HIDDEN: 'hidden' + EVENT_KEY,
      CLICK_DATA_API: 'click' + EVENT_KEY + DATA_API_KEY
    };

    var MetisMenu = function () {
      function MetisMenu(element, config) {
        _classCallCheck(this, MetisMenu);

        this._element = element;
        this._config = this._getConfig(config);
        this._transitioning = null;

        this.init();
      }

      MetisMenu.prototype.init = function init() {
        var self = this;
        $(this._element).find(this._config.parentTrigger2 + '.' + this._config.activeClass2).has(this._config.subMenu2).children(this._config.subMenu2).attr('ariaexpanded', true).addClass(this._config.collapseClass2 + ' ' + this._config.collapseInClass2);

        $(this._element).find(this._config.parentTrigger2).not('.' + this._config.activeClass2).has(this._config.subMenu2).children(this._config.subMenu2).attr('ariaexpanded', false).addClass(this._config.collapseClass2);

        $(this._element).find(this._config.parentTrigger2).has(this._config.subMenu2).children(this._config.triggerElement2).on(Event.CLICK_DATA_API, function (e) {
          var _this = $(this);
          var _parent = _this.parent(self._config.parentTrigger2);
          var _siblings = _parent.siblings(self._config.parentTrigger2).children(self._config.triggerElement2);
          var _list = _parent.children(self._config.subMenu2);
          if (self._config.preventDefault) {
            e.preventDefault();
          }
          if (_this.attr('ariadisabled') === 'true') {
            return;
          }
          if (_parent.hasClass(self._config.activeClass2)) {
            _this.attr('ariaexpanded', false);
            self._hide(_list);
          } else {
            self._show(_list);
            _this.attr('ariaexpanded', true);
            if (self._config.toggle2) {
              _siblings.attr('ariaexpanded', false);
            }
          }

          if (self._config.onTransitionStart) {
            self._config.onTransitionStart(e);
          }
        });
      };

      MetisMenu.prototype._show = function _show(element) {
        if (this._transitioning || $(element).hasClass(this._config.collapsingClass2)) {
          return;
        }
        var _this = this;
        var _el = $(element);

        var startEvent = $.Event(Event.SHOW);
        _el.trigger(startEvent);

        if (startEvent.isDefaultPrevented()) {
          return;
        }

        _el.parent(this._config.parentTrigger2).addClass(this._config.activeClass2);

        if (this._config.toggle2) {
          this._hide(_el.parent(this._config.parentTrigger2).siblings().children(this._config.subMenu2 + '.' + this._config.collapseInClass2).attr('ariaexpanded', false));
        }

        _el.removeClass(this._config.collapseClass2).addClass(this._config.collapsingClass2).height(0);

        this.setTransitioning(true);

        var complete = function complete() {

          _el.removeClass(_this._config.collapsingClass2).addClass(_this._config.collapseClass2 + ' ' + _this._config.collapseInClass2).height('').attr('ariaexpanded', true);

          _this.setTransitioning(false);

          _el.trigger(Event.SHOWN);
        };

        if (!Util2.supportsTransitionEnd()) {
          complete();
          return;
        }

        _el.height(_el[0].scrollHeight).one(Util2.TRANSITION_END, complete).emulateTransitionEnd(TRANSITION_DURATION);
      };

      MetisMenu.prototype._hide = function _hide(element) {

        if (this._transitioning || !$(element).hasClass(this._config.collapseInClass2)) {
          return;
        }
        var _this = this;
        var _el = $(element);

        var startEvent = $.Event(Event.HIDE);
        _el.trigger(startEvent);

        if (startEvent.isDefaultPrevented()) {
          return;
        }

        _el.parent(this._config.parentTrigger2).removeClass(this._config.activeClass2);
        _el.height(_el.height())[0].offsetHeight;

        _el.addClass(this._config.collapsingClass2).removeClass(this._config.collapseClass2).removeClass(this._config.collapseInClass2);

        this.setTransitioning(true);

        var complete = function complete() {
          if (_this._transitioning && _this._config.onTransitionEnd) {
            _this._config.onTransitionEnd();
          }

          _this.setTransitioning(false);
          _el.trigger(Event.HIDDEN);

          _el.removeClass(_this._config.collapsingClass2).addClass(_this._config.collapseClass2).attr('ariaexpanded', false);
        };

        if (!Util2.supportsTransitionEnd()) {
          complete();
          return;
        }

        _el.height() == 0 || _el.css('display') == 'none' ? complete() : _el.height(0).one(Util2.TRANSITION_END, complete).emulateTransitionEnd(TRANSITION_DURATION);
      };

      MetisMenu.prototype.setTransitioning = function setTransitioning(isTransitioning) {
        this._transitioning = isTransitioning;
      };

      MetisMenu.prototype.dispose = function dispose() {
        $.removeData(this._element, DATA_KEY);

        $(this._element).find(this._config.parentTrigger2).has(this._config.subMenu2).children(this._config.triggerElement2).off('click');

        this._transitioning = null;
        this._config = null;
        this._element = null;
      };

      MetisMenu.prototype._getConfig = function _getConfig(config) {
        config = $.extend({}, Default2, config);
        return config;
      };

      MetisMenu._jQueryInterface = function _jQueryInterface(config) {
        return this.each(function () {
          var $this = $(this);
          var data = $this.data(DATA_KEY);
          var _config = $.extend({}, Default2, $this.data(), (typeof config === 'undefined' ? 'undefined' : _typeof(config)) === 'object' && config);

          if (!data && /dispose/.test(config)) {
            this.dispose();
          }

          if (!data) {
            data = new MetisMenu(this, _config);
            $this.data(DATA_KEY, data);
          }

          if (typeof config === 'string') {
            if (data[config] === undefined) {
              throw new Error('No method named "' + config + '"');
            }
            data[config]();
          }
        });
      };

      return MetisMenu;
    }();

    /**
     * ------------------------------------------------------------------------
     * jQuery
     * ------------------------------------------------------------------------
     */

    $.fn[NAME] = MetisMenu._jQueryInterface;
    $.fn[NAME].Constructor = MetisMenu;
    $.fn[NAME].noConflict = function () {
      $.fn[NAME] = JQUERY_NO_CONFLICT;
      return MetisMenu._jQueryInterface;
    };
    return MetisMenu;
  }(jQuery);
});