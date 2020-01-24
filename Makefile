CSC		= /cygdrive/c/windows/microsoft.net/framework/v4.0.30319/csc.exe
TARGET	= MR.exe
SRC		= \
	src\\main.cs \

DEPS	= \
	res\\manifest.xml \

CSC_FLAGS		= \
	/nologo \
	/win32manifest:res\\manifest.xml \
	/target:winexe

DEBUG_FLAGS		= 
RELEASE_FLAGS	= 

.PHONY: debug
debug: CSC_FLAGS+=$(DEBUG_FLAGS)
debug: all

.PHONY: release
release: CSC_FLAGS+=$(RELEASE_FLAGS)
release: all

.PHONY: genzip
genzip:
	zip -r MRSwitcher.zip MRSwitcher

all: $(TARGET)
$(TARGET): $(SRC) $(DEPS)
	$(CSC) $(CSC_FLAGS) /out:$(TARGET) $(SRC)

.PHONY: clean
clean:
	rm $(TARGET)


