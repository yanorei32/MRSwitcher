PROJECT_NAME	= MRSwitcher
RELEASE_BASE	= release
RELEASE_DIR		= $(RELEASE_BASE)/$(PROJECT_NAME)


########################################
# Build
########################################

all: control launcher

CSC			= /cygdrive/c/Windows/Microsoft.NET/Framework/v4.0.30319/csc.exe
CSC_FLAGS	= /nologo

CONTROL_SRCS		= control\\src\\main.cs
CONTROL_DEPS		= 
CONTROL_CSC_FLAGS	= 
CONTROL_TARGET		= $(RELEASE_DIR)/mr-ctl.exe

.PHONY: control
control: $(CONTROL_TARGET)

$(CONTROL_TARGET): $(CONTROL_SRCS) $(CONTROL_DEPS)
	$(CSC) \
		$(CSC_FLAGS) \
		$(CONTROL_CSC_FLAGS) \
		-out:$(CONTROL_TARGET) \
		$(CONTROL_SRCS)

.PHONY: control_clean
control_clean:
	rm -f $(CONTROL_TARGET)

LAUNCHER_SRCS				= launcher\\src\\main.cs
LAUNCHER_DEPS				= launcher\\res\\manifest.xml
LAUNCHER_CSC_FLAGS			= /target:winexe \
							  -win32manifest:launcher/res/manifest.xml
LAUNCHER_ENABLER_TARGET		= $(RELEASE_DIR)/mr-enable.exe
LAUNCHER_ENABLER_CSC_FLAGS	= -define:ENABLER
LAUNCHER_DISABLER_TARGET	= $(RELEASE_DIR)/mr-disable.exe
LAUNCHER_DISABLER_CSC_FLAGS	= -define:DISABLER
LAUNCHER_TARGETS			= $(LAUNCHER_ENABLER_TARGET) \
							  $(LAUNCHER_DISABLER_TARGET)

$(LAUNCHER_ENABLER_TARGET): $(LAUNCHER_SRCS) $(LAUNCHER_DEPS)
	$(CSC) \
		$(CSC_FLAGS) \
		$(LAUNCHER_CSC_FLAGS) \
		$(LAUNCHER_ENABLER_CSC_FLAGS) \
		-out:$@ \
		$(LAUNCHER_SRCS)

$(LAUNCHER_DISABLER_TARGET): $(LAUNCHER_SRCS) $(LAUNCHER_DEPS)
	$(CSC) \
		$(CSC_FLAGS) \
		$(LAUNCHER_CSC_FLAGS) \
		$(LAUNCHER_DISABLER_CSC_FLAGS) \
		-out:$@ \
		$(LAUNCHER_SRCS)

.PHONY: launcher
launcher: $(LAUNCHER_TARGETS)

.PHONY: launcher_clean
launcher_clean:
	rm -f $(LAUNCHER_TARGETS)

.PHONY: build_clean
build_clean: control_clean launcher_clean


########################################
# Zip
########################################

ZIP_NAME		= $(PROJECT_NAME).zip
ZIP_TARGET		= $(RELEASE_BASE)/$(ZIP_NAME)
ZIP_DEPS		= LICENSE README.md
BUILD_TARGETS	= $(CONTROL_TARGET) $(LAUNCHER_TARGETS)

genzip: $(ZIP_TARGET)
$(ZIP_TARGET): $(BUILD_TARGETS) $(ZIP_DEPS)
	cd $(RELEASE_BASE); \
		zip -r $(ZIP_NAME) $(PROJECT_NAME)

.PHONY: zip_clean
zip_clean:
	rm -f $(ZIP_TARGET)


########################################
# Clean
########################################

.PHONY: clean
clean: build_clean zip_clean

